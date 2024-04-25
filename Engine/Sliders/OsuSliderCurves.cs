using online_osu_beatmap_editor_client.Engine.GameplayElements.Objects;
using SFML.Graphics.Glsl;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace online_osu_beatmap_editor_client.Engine.Sliders
{
	public static class GlobalVar
	{
		public static float osu_slider_curve_points_separation = 2.5f;//"slider body curve approximation step width in osu!pixels, don't set this lower than around 1.5");
        public static float osu_slider_curve_max_points = 9999.0f;//"maximum number of allowed interpolated curve points. quality will be forced to go down if a slider has more steps than this");
        public static float osu_slider_curve_max_length = 65536/2;//"maximum slider length in osu!pixels (i.e. pixelLength). also used to clamp all (control-)point coordinates to sane values.");
	}

    public class OsuSliderCurve
    {
		public float m_fPixelLength;
		public List<Vector2> m_controlPoints;

		// these must be explicitly calculated/set in one of the subclasses
		public List<List<Vector2>> m_curvePointSegments;
		public List<List<Vector2>> m_originalCurvePointSegments;
		public List<Vector2> m_curvePoints;
		public List<Vector2> m_originalCurvePoints;
		public float m_fStartAngle;
		public float m_fEndAngle;

		enum OSUSLIDERCURVETYPE
		{
			OSUSLIDERCURVETYPE_CATMULL = 'C',
			OSUSLIDERCURVETYPE_BEZIER = 'B',
			OSUSLIDERCURVETYPE_LINEAR = 'L',
			OSUSLIDERCURVETYPE_PASSTHROUGH = 'P'
		};

        OsuSliderCurve createCurve(char osuSliderCurveType, List<Vector2> controlPoints, float pixelLength) { 
			return createCurve(osuSliderCurveType, controlPoints, pixelLength, GlobalVar.osu_slider_curve_points_separation);
		}

		OsuSliderCurve createCurve(char osuSliderCurveType, List<Vector2> controlPoints, float pixelLength, float curvePointsSeparation)
		{
			if ((osuSliderCurveType == (char)OSUSLIDERCURVETYPE.OSUSLIDERCURVETYPE_PASSTHROUGH) && controlPoints.Count == 3)
			{
				Vector2 nora = controlPoints[1] - controlPoints[0];
				Vector2 norb = controlPoints[1] - controlPoints[2];

				float temp = nora.X;
				nora.X = -nora.Y;
				nora.Y = temp;
				temp = norb.X;
				norb.X = -norb.Y;
				norb.Y = temp;

				// TODO: to properlY support all aspire sliders (e.g. Ping), need to use osu circular arc calc + subdivide line segments if theY are too big

				if (Math.Abs(norb.X * nora.Y - norb.Y * nora.X) < 0.00001f)
					return new OsuSliderCurveLinearBezier(controlPoints, pixelLength, false, curvePointsSeparation); // vectors parallel, use linear bezier instead
				else
					return new OsuSliderCurveCircumscribedCircle(controlPoints, pixelLength, curvePointsSeparation);
			}
			//else if (osuSliderCurveType == (char)OSUSLIDERCURVETYPE.OSUSLIDERCURVETYPE_CATMULL)
			//	  return new OsuSliderCurveCatmull(controlPoints, pixelLength, curvePointsSeparation);
			else
				return new OsuSliderCurveLinearBezier(controlPoints, pixelLength, (osuSliderCurveType == (char)OSUSLIDERCURVETYPE.OSUSLIDERCURVETYPE_LINEAR), curvePointsSeparation);
		}

		public OsuSliderCurve(List<Vector2> controlPoints, float pixelLength)
		{
			m_controlPoints = controlPoints;
			m_fPixelLength = pixelLength;

			m_fStartAngle = 0.0f;
			m_fEndAngle = 0.0f;
		}

		void updateStackPosition(float stackMulStackOffset, bool HR)
		{
			for (int i=0; i<m_originalCurvePoints.Count() && i<m_curvePoints.Count(); i++)
			{
				m_curvePoints[i] = m_originalCurvePoints[i] - new Vector2(stackMulStackOffset, stackMulStackOffset * (HR ? -1.0f : 1.0f));
			}

			for (int s=0; s<m_originalCurvePointSegments.Count() && s<m_curvePointSegments.Count(); s++)
			{
				for (int p=0; p<m_originalCurvePointSegments[s].Count() && p<m_curvePointSegments[s].Count(); p++)
				{
					m_curvePointSegments[s][p] = m_originalCurvePointSegments[s][p] - new Vector2(stackMulStackOffset, stackMulStackOffset * (HR ? -1.0f : 1.0f));
				}
			}
		}
    }

    public class OsuSliderCurveType
    {
        public List<Vector2> m_points;
		public float m_fTotalDistance;
		public List<float> m_curveDistances;

        public virtual Vector2 pointAt(float t) { return new(); }

        public OsuSliderCurveType()
		{
			m_fTotalDistance = 0.0f;
		}

		void init(float approxLength)
		{
			calculateIntermediaryPoints(approxLength);
			calculateCurveDistances();
		}

		void initCustom(List<Vector2> points)
		{
			m_points = points;
			calculateCurveDistances();
		}

		void calculateIntermediaryPoints(float approxLength)
		{
			// subdivide the curve, calculate all intermediary points
			int numPoints = (int)(approxLength / 4.0f) + 2;
			for (int i=0; i<numPoints; i++)
			{
				m_points.Add(pointAt(i / (float)(numPoints - 1)));
			}
		}

		void calculateCurveDistances()
		{
			// reset
			m_fTotalDistance = 0.0f;
			m_curveDistances.Clear();

			// find the distance of each point from the previous point (needed for some curve types)
			for (int i=0; i<m_points.Count(); i++)
			{
				float curDist = (i == 0) ? 0 : (m_points[i] - m_points[i-1]).Length();

				m_curveDistances.Add(curDist);
				m_fTotalDistance += curDist;
            }
        }
    }

	public class OsuSliderCurveTypeBezier2(List<Vector2> points) : OsuSliderCurveType()
	{
		public void initCustom(OsuSliderBezierApproximator().createBezier(points));
	}

    public class OsuSliderCurveEqualDistanceMulti : OsuSliderCurve
    {
		int m_iNCurve;

		public OsuSliderCurveEqualDistanceMulti(List<Vector2> controlPoints, float pixelLength, float curvePointsSeparation) : base (controlPoints, pixelLength)
		{
			m_iNCurve = Math.Min((int)(m_fPixelLength / OsuMath.Clamp(curvePointsSeparation, 1.0f, 100.0f)), (int)GlobalVar.osu_slider_curve_max_points);
		}

		public unsafe void init(ref OsuSliderCurveType*[] curvesList)
		{
			if (curvesList.Length < 1)
			{
				Console.WriteLine("OsuSliderCurveEqualDistanceMulti::init() Error: curvesList.Count == 0!!!\n");
				return;
			}

			int curCurveIndex = 0;
			int curPoint = 0;

			float distanceAt = 0.0f;
			float lastDistanceAt = 0.0f;

			OsuSliderCurveType* curCurve = curvesList[curCurveIndex];
			{
				if (curCurve->m_points.Count < 1)
				{
					Console.WriteLine("OsuSliderCurveEqualDistanceMulti::init() Error: curCurve->m_points.Count == 0!!!\n");
					return;
				}
			}
			Vector2 lastCurve = curCurve->m_points[curPoint];

			// length of the curve should be equal to the pixel length
			// for each distance, try to get in between the two points that are between it
			Vector2 lastCurvePointForNextSegmentStart = new();
			List<Vector2> curCurvePoints = new();
			for (int i=0; i<(m_iNCurve + 1); i++)
			{
				int prefDistance = (int)(((float)i * m_fPixelLength) / (float)m_iNCurve);
				while (distanceAt < prefDistance)
				{
					lastDistanceAt = distanceAt;
					if (curCurve->m_points.Count > 0 && curPoint > -1 && curPoint < curCurve->m_points.Count)
						lastCurve = curCurve->m_points[curPoint];

					// jump to next point
					curPoint++;

					if (curPoint >= curCurve->m_points.Count)
					{
						// jump to next curve
						curCurveIndex++;

						// when jumping to the next curve, add the current segment to the list of segments
						if (curCurvePoints.Count > 0)
						{
							m_curvePointSegments.Add(curCurvePoints);
							curCurvePoints.Clear();

							// prepare the next segment by setting/adding the starting point to the exact end point of the previous segment
							// this also enables an optimization, namely that startcaps only have to be drawn [for every segment] if the startpoint != endpoint in the loop
							if (m_curvePoints.Count > 0)
								curCurvePoints.Add(lastCurvePointForNextSegmentStart);
						}

						if (curCurveIndex < curvesList.Length)
						{
							curCurve = curvesList[curCurveIndex];
							curPoint = 0;
						}
						else
						{
							curPoint = curCurve->m_points.Count - 1;
							if (lastDistanceAt == distanceAt)
							{
								// out of points even though the preferred distance hasn't been reached
									break;
							}
						}
					}
					if (curCurve->m_curveDistances.Count > 0 && curPoint > -1 && curPoint < curCurve->m_curveDistances.Count)
						distanceAt += curCurve->m_curveDistances[curPoint];
				}
				Vector2 thisCurve = (curCurve->m_points.Count > 0 && curPoint > -1 && curPoint < curCurve->m_points.Count ? curCurve->m_points[curPoint] : new Vector2(0, 0));

				// interpolate the point between the two closest distances
					m_curvePoints.Add(new Vector2(0, 0));
					curCurvePoints.Add(new Vector2(0, 0));
					if (distanceAt - lastDistanceAt > 1)
					{
						float t = (prefDistance - lastDistanceAt) / (distanceAt - lastDistanceAt);
						m_curvePoints[i] = new Vector2(OsuMath.Lerp(lastCurve.X, thisCurve.X, t), OsuMath.Lerp(lastCurve.Y, thisCurve.Y, t));
					}
					else
						m_curvePoints[i] = thisCurve;

					// add the point to the current segment (this is not using the lerp'd point! would cause mm mesh artifacts if it did)
					lastCurvePointForNextSegmentStart = thisCurve;
					curCurvePoints[curCurvePoints.Count - 1] = thisCurve;
			}

			// if we only had one segment, no jump to any next curve has occurred (and therefore no insertion of the segment into the vector)
			// manually add the lone segment here
			if (curCurvePoints.Count > 0)
				m_curvePointSegments.Add(curCurvePoints);

			// sanity check
			if (m_curvePoints.Count == 0)
			{
				Console.WriteLine("OsuSliderCurveEqualDistanceMulti::init() Error: m_curvePoints.Count == 0!!!\n");
				return;
			}

			// make sure that the uninterpolated segment points are exactly as long as the pixelLength
			// this is necessary because we can't use the lerp'd points for the segments
			float segmentedLength = 0.0f;
			for (int s=0; s<m_curvePointSegments.Count; s++)
			{
				for (int p=0; p<m_curvePointSegments[s].Count; p++)
				{
					segmentedLength += ((p == 0) ? 0 : (m_curvePointSegments[s][p] - m_curvePointSegments[s][p-1]).Length());
				}
			}

			// TODO: this is still incorrect. sliders are sometimes too long or start too late, even if only for a few pixels
			if (segmentedLength > m_fPixelLength && m_curvePointSegments.Count > 1 && m_curvePointSegments[0].Count > 1)
			{
				float excess = segmentedLength - m_fPixelLength;
				while (excess > 0)
				{
					for (int s=m_curvePointSegments.Count-1; s>=0; s--)
					{
						for (int p=m_curvePointSegments[s].Count-1; p>=0; p--)
						{
							float curLength = (p == 0) ? 0 : (m_curvePointSegments[s][p] - m_curvePointSegments[s][p-1]).Length();
							if (curLength >= excess && p != 0)
							{
								Vector2 segmentVector = Vector2.Normalize(m_curvePointSegments[s][p] - m_curvePointSegments[s][p-1]);
								m_curvePointSegments[s][p] = m_curvePointSegments[s][p] - segmentVector*excess;
								excess = 0.0f;
								break;
							}
							else
							{
								m_curvePointSegments[s].Remove(m_curvePointSegments[s][p]);
								excess -= curLength;
							}
						}
					}
				}
			}

			// calculate start and end angles for possible repeats (good enough and cheaper than calculating it live every frame)
			if (m_curvePoints.Count > 1)
			{
				Vector2 c1 = m_curvePoints[0];
				int cnt = 1;
				Vector2 c2 = m_curvePoints[cnt++];
				while (cnt <= m_iNCurve && cnt < m_curvePoints.Count && (c2-c1).Length() < 1)
				{
					c2 = m_curvePoints[cnt++];
				}
				m_fStartAngle = (float)(Math.Atan2(c2.Y - c1.Y, c2.X - c1.X) * 180 / Math.PI);
			}

			if (m_curvePoints.Count > 1)
			{
				if (m_iNCurve < m_curvePoints.Count)
				{
					Vector2 c1 = m_curvePoints[m_iNCurve];
					int cnt = m_iNCurve - 1;
					Vector2 c2 = m_curvePoints[cnt--];
					while (cnt >= 0 && (c2-c1).Length() < 1)
					{
						c2 = m_curvePoints[cnt--];
					}
					m_fEndAngle = (float)(Math.Atan2(c2.Y - c1.Y, c2.X - c1.X) * 180 / Math.PI);
				}
			}

			// backup (for dynamic updateStackPosition() recalculation)
			List<Vector2> m_originalCurvePoints = m_curvePoints; // copy
			List<List<Vector2>> m_originalCurvePointSegments = m_curvePointSegments; // copy
		}

		Vector2 pointAt(float t)
		{
			if (m_curvePoints.Count < 1) return new Vector2(0, 0);

			float indexF = t * m_iNCurve;
			int index = (int)indexF;
			if (index >= m_iNCurve)
			{
				if (m_iNCurve > -1 && m_iNCurve < m_curvePoints.Count)
					return m_curvePoints[m_iNCurve];
				else
				{
					Console.WriteLine("OsuSliderCurveEqualDistanceMulti::pointAt() Error: Illegal index %i!!!\n", m_iNCurve);
					return new Vector2(0, 0);
				}
			}
			else
			{
				if (index < 0 || index + 1 >= m_curvePoints.Count)
				{
					Console.WriteLine("OsuSliderCurveEqualDistanceMulti::pointAt() Error: Illegal index %i!!!\n", index);
					return new Vector2(0, 0);
				}

				Vector2 poi = m_curvePoints[index];
				Vector2 poi2 = m_curvePoints[index + 1];

				float t2 = indexF - index;

				return new Vector2(OsuMath.Lerp(poi.X, poi2.X, t2), OsuMath.Lerp(poi.Y, poi2.Y, t2));
			}
		}

		Vector2 originalPointAt(float t)
		{
			if (m_originalCurvePoints.Count < 1) return new Vector2(0, 0);

			float indexF = t * m_iNCurve;
			int index = (int)indexF;
			if (index >= m_iNCurve)
			{
				if (m_iNCurve > -1 && m_iNCurve < m_originalCurvePoints.Count)
					return m_originalCurvePoints[m_iNCurve];
				else
				{
					Console.WriteLine("OsuSliderCurveEqualDistanceMulti::originalPointAt() Error: Illegal index %i!!!\n", m_iNCurve);
					return new Vector2(0, 0);
				}
			}
			else
			{
				if (index < 0 || index + 1 >= m_originalCurvePoints.Count)
				{
					Console.WriteLine("OsuSliderCurveEqualDistanceMulti::originalPointAt() Error: Illegal index %i!!!\n", index);
					return new Vector2(0, 0);
				}

				Vector2 poi = m_originalCurvePoints[index];
				Vector2 poi2 = m_originalCurvePoints[index + 1];

				float t2 = indexF - index;

				return new Vector2(OsuMath.Lerp(poi.X, poi2.X, t2), OsuMath.Lerp(poi.Y, poi2.Y, t2));
			}
		}
	}

	public class OsuSliderCurveLinearBezier : OsuSliderCurveEqualDistanceMulti 
	{ 
		public unsafe OsuSliderCurveLinearBezier(List<Vector2> controlPoints, float pixelLength, bool line, float curvePointsSeparation) : base(controlPoints, pixelLength, curvePointsSeparation)
		{
			int numControlPoints = m_controlPoints.Count;

			OsuSliderCurveType*[] beziers = new OsuSliderCurveType*[numControlPoints+1];

			List<Vector2> points = new(); // temporary list of points to separate different bezier curves

			// Beziers: splits points into different Beziers if has the same points (red points)
			// a b c - c d - d e f g
			// Lines: generate a new curve for each sequential pair
			// ab  bc  cd  de  ef  fg
			Vector2 lastPoint = new();
			for (int i=0; i<numControlPoints; i++)
			{
				Vector2 currentPoint = m_controlPoints[i];

				if (line)
				{
					if (i > 0)
					{
						points.Add(currentPoint);

						beziers[i] = new OsuSliderCurveTypeBezier2(points);

						points.Clear();
					}
				}
				else if (i > 0)
				{
					if (currentPoint == lastPoint)
					{
						if (points.Count >= 2)
						{
							beziers[i] = new OsuSliderCurveTypeBezier2(points);
						}

						points.Clear();
					}
				}

				points.Add(currentPoint);
				lastPoint = currentPoint;
			}

			if (line || points.Count < 2)
			{
				// trying to continue Bezier with less than 2 points
				// probably ending on a red point, just ignore it
			}
			else
			{
				beziers[beziers.Length] = new OsuSliderCurveTypeBezier2(points);

				points.Clear();
			}

			//MY ADDITION: This may not be necessary, but working with pointers and std::vector got replaced with array bcs c#, so make sure no nulls in array
			OsuSliderCurveType*[] result = new OsuSliderCurveType*[beziers.Length-1];
			if (beziers[beziers.Length] == null)
			{
				Array.Copy(beziers, 0, result, 0, result.Length);
			}
			//MY ADDITION

			// build curve
			base.init(ref beziers);
		}
	}

	public class OsuSliderCurveCatmull : OsuSliderCurveEqualDistanceMulti 
	{
		public unsafe OsuSliderCurveCatmull(List<Vector2> controlPoints, float pixelLength, float curvePointsSeparation) : base(controlPoints, pixelLength, curvePointsSeparation)
		{
			int numControlPoints = m_controlPoints.Count;

			OsuSliderCurveType*[] catmulls = new OsuSliderCurveType*[numControlPoints+1];;

			List<Vector2> points = new(); // temporary list of points to separate different curves

			// repeat the first and last points as controls points
			// only if the first/last two points are different
			// aabb
			// aabc abcc
			// aabc abcd bcdd
			if (m_controlPoints[0].X != m_controlPoints[1].X || m_controlPoints[0].Y != m_controlPoints[1].Y)
				points.Add(m_controlPoints[0]);

			for (int i=0; i<numControlPoints; i++)
			{
				points.Add(m_controlPoints[i]);
				if (points.Count >= 4)
				{
					catmulls[i] = new OsuSliderCurveTypeCentripetalCatmullRom(points);

					points.Remove(points.First());
				}
			}

			if (m_controlPoints[numControlPoints - 1].X != m_controlPoints[numControlPoints - 2].X || m_controlPoints[numControlPoints - 1].Y != m_controlPoints[numControlPoints - 2].Y)
				points.Add(m_controlPoints[numControlPoints - 1]);

			if (points.Count >= 4)
			{
				catmulls[catmulls.Length] = (new OsuSliderCurveTypeCentripetalCatmullRom(points));
			}

			// build curve
			base.init(ref catmulls);
		}

		public class OsuSliderCurveCircumscribedCircle : OsuSliderCurve
		{
			public unsafe OsuSliderCurveCircumscribedCircle(List<Vector2> controlPoints, float pixelLength, float curvePointsSeparation) : base(controlPoints, pixelLength)
			{
				if (controlPoints.Count != 3)
				{
					Console.WriteLine("OsuSliderCurveCircumscribedCircle() Error: controlPoints.size() != 3\n");
					return;
				}

				// construct the three points
				Vector2 start = m_controlPoints[0];
				Vector2 mid = m_controlPoints[1];
				Vector2 end = m_controlPoints[2];

				// find the circle center
				Vector2 mida = start + (mid-start)*0.5f;
				Vector2 midb = end + (mid-end)*0.5f;

				Vector2 nora = mid - start;
				float temp = nora.X;
				nora.X = -nora.Y;
				nora.Y = temp;
				Vector2 norb = mid - end;
				temp = norb.X;
				norb.X = -norb.Y;
				norb.Y = temp;

				m_vOriginalCircleCenter = intersect(mida, nora, midb, norb);
				m_vCircleCenter = m_vOriginalCircleCenter;

				// find the angles relative to the circle center
				Vector2 startAngPoint = start - m_vCircleCenter;
				Vector2 midAngPoint   = mid - m_vCircleCenter;
				Vector2 endAngPoint   = end - m_vCircleCenter;

				m_fCalculationStartAngle = (float)std::atan2(startAngPoint.y, startAngPoint.x);
				const float midAng		 = (float)std::atan2(midAngPoint.y, midAngPoint.x);
				m_fCalculationEndAngle	 = (float)std::atan2(endAngPoint.y, endAngPoint.x);

				// find the angles that pass through midAng
				if (!isIn(m_fCalculationStartAngle, midAng, m_fCalculationEndAngle))
				{
					if (std::abs(m_fCalculationStartAngle + 2*PI - m_fCalculationEndAngle) < 2*PI && isIn(m_fCalculationStartAngle + (2*PI), midAng, m_fCalculationEndAngle))
						m_fCalculationStartAngle += 2*PI;
					else if (std::abs(m_fCalculationStartAngle - (m_fCalculationEndAngle + 2*PI)) < 2*PI && isIn(m_fCalculationStartAngle, midAng, m_fCalculationEndAngle + (2*PI)))
						m_fCalculationEndAngle += 2*PI;
					else if (std::abs(m_fCalculationStartAngle - 2*PI - m_fCalculationEndAngle) < 2*PI && isIn(m_fCalculationStartAngle - (2*PI), midAng, m_fCalculationEndAngle))
						m_fCalculationStartAngle -= 2*PI;
					else if (std::abs(m_fCalculationStartAngle - (m_fCalculationEndAngle - 2*PI)) < 2*PI && isIn(m_fCalculationStartAngle, midAng, m_fCalculationEndAngle - (2*PI)))
						m_fCalculationEndAngle -= 2*PI;
					else
					{
						debugLog("OsuSliderCurveCircumscribedCircle() Error: Cannot find angles between midAng (%.3f %.3f %.3f)\n", m_fCalculationStartAngle, midAng, m_fCalculationEndAngle);
						return;
					}
				}

				// find an angle with an arc length of pixelLength along this circle
				m_fRadius = startAngPoint.length();
				const float arcAng = m_fPixelLength / m_fRadius;  // len = theta * r / theta = len / r

				// now use it for our new end angle
				m_fCalculationEndAngle = (m_fCalculationEndAngle > m_fCalculationStartAngle) ? m_fCalculationStartAngle + arcAng : m_fCalculationStartAngle - arcAng;

				// find the angles to draw for repeats
				m_fEndAngle   = (float)((m_fCalculationEndAngle   + (m_fCalculationStartAngle > m_fCalculationEndAngle ? PI/2.0f : -PI/2.0f)) * 180.0f / PI);
				m_fStartAngle = (float)((m_fCalculationStartAngle + (m_fCalculationStartAngle > m_fCalculationEndAngle ? -PI/2.0f : PI/2.0f)) * 180.0f / PI);

				// calculate points
				const float steps = std::min(m_fPixelLength / (clamp<float>(curvePointsSeparation, 1.0f, 100.0f)), osu_slider_curve_max_points.getFloat());
				const int intSteps = (int)std::round(steps) + 2; // must guarantee an int range of 0 to steps!
				for (int i=0; i<intSteps; i++)
				{
					float t = clamp<float>((float)i / steps, 0.0f, 1.0f);
					m_curvePoints.push_back(pointAt(t));

					if (t >= 1.0f) // early break if we've already reached the end
						break;
				}

				// add the segment (no special logic here for SliderCurveCircumscribedCircle, just add the entire vector)
				m_curvePointSegments.push_back(std::vector<Vector2>(m_curvePoints)); // copy

				// backup (for dynamic updateStackPosition() recalculation)
				m_originalCurvePoints = std::vector<Vector2>(m_curvePoints); // copy
				m_originalCurvePointSegments = std::vector<std::vector<Vector2>>(m_curvePointSegments); // copy
			}
		}
	}

	public class OsuSliderBezierApproximator
	{
		static double TOLERANCE_SQ;
		int m_iCount;
		List<Vector2> m_subdivisionBuffer1;
		List<Vector2> m_subdivisionBuffer2;

		public OsuSliderBezierApproximator()
		{
			m_iCount = 0;
		}

		public List<Vector2> createBezier(List<Vector2> controlPoints)
		{
			m_iCount = controlPoints.Count;

			List<Vector2> output = new();
			if (m_iCount == 0) return output;

			Stack<List<Vector2>> toFlatten = new();
			Stack<List<Vector2>> freeBuffers = new();

			toFlatten.Push(controlPoints); // copy

			List<Vector2> leftChild = m_subdivisionBuffer2;

			while (toFlatten.Count > 0)
			{
				List<Vector2> parent = toFlatten.Peek();
				toFlatten.Pop();

				if (isFlatEnough(parent))
				{
					approximate(parent, output);
					freeBuffers.Push(parent);
					continue;
				}

				List<Vector2> rightChild = new();
				if (freeBuffers.Count > 0)
				{
					rightChild = freeBuffers.Peek();
					freeBuffers.Pop();
				}

				subdivide(parent, leftChild, rightChild);

				for (int i=0; i<m_iCount; ++i)
				{
					parent[i] = leftChild[i];
				}

				toFlatten.Push(std::move(rightChild));
				toFlatten.Push(std::move(parent));
			}

			output.Push_back(controlPoints[m_iCount - 1]);
			return output;
		}

		bool isFlatEnough(const std::vector<Vector2> &controlPoints)
		{
			if (controlPoints.size() < 1) return true;

			for (int i=1; i<(int)(controlPoints.size() - 1); i++)
			{
				if (std::pow((double)(controlPoints[i - 1] - 2 * controlPoints[i] + controlPoints[i + 1]).length(), 2.0) > TOLERANCE_SQ * 4)
					return false;
			}

			return true;
		}

		void subdivide(std::vector<Vector2> &controlPoints, std::vector<Vector2> &l, std::vector<Vector2> &r)
		{
			std::vector<Vector2> &midpoints = m_subdivisionBuffer1;

			for (int i=0; i<m_iCount; ++i)
			{
				midpoints[i] = controlPoints[i];
			}

			for (int i=0; i<m_iCount; i++)
			{
				l[i] = midpoints[0];
				r[m_iCount - i - 1] = midpoints[m_iCount - i - 1];

				for (int j=0; j<m_iCount-i-1; j++)
				{
					midpoints[j] = (midpoints[j] + midpoints[j + 1]) / 2;
				}
			}
		}

		void approximate(std::vector<Vector2> &controlPoints, std::vector<Vector2> &output)
		{
			std::vector<Vector2> &l = m_subdivisionBuffer2;
			std::vector<Vector2> &r = m_subdivisionBuffer1;

			subdivide(controlPoints, l, r);

			for (int i=0; i<m_iCount-1; ++i)
			{
				l[m_iCount + i] = r[i + 1];
			}

			output.push_back(controlPoints[0]);
			for (int i=1; i<m_iCount-1; ++i)
			{
				const int index = 2 * i;
				Vector2 p = 0.25f * (l[index - 1] + 2 * l[index] + l[index + 1]);
				output.push_back(p);
			}
		}
	}
}
