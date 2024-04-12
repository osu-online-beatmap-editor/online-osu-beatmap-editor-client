﻿using online_osu_beatmap_editor_client.Engine.GameplayElements;
using online_osu_beatmap_editor_client.Engine.GameplayElements.Beatmap;
using online_osu_beatmap_editor_client.Engine.GameplayElements.Colours;
using online_osu_beatmap_editor_client.Engine.GameplayElements.Objects;
using online_osu_beatmap_editor_client.Engine.GameplayElements.Timing;
using online_osu_beatmap_editor_client.gameplay_elements.Audio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Globalization;
using System.Linq;

namespace online_osu_beatmap_editor_client.Engine.Beatmap
{
    public static class BeatmapParser
    {
        public static void ParseBeatmap(string[] lines)
        {
            BeatmapData.general.gameFileVersion = lines[0];

            List<Action<string[]>> segments = new()
            {
                {General},
                {Editor},
                {Metadata},
                {Difficulty},
                {Events},
                {TimingPoints},
                {(x)=>{}},
                {Colours},
                {HitObjects}
            }; 
            int current = 0;

            List<string> segment = new();
            for (int i=3; i<lines.Length; i++)
            {
                string l = lines[i].Trim().TrimEnd('\r','\n');

                if (l == "")
                {
                    segments[current++]([.. segment]);
                    segment.Clear();
                }
                else
                {
                    segment.Add(l);
                }
            }

            segments[current++]([.. segment]);

            Console.WriteLine("a");
        }

        private static void General(string[] lines)
        { 
            int i=0;
            BeatmapData.general.AudioFileName = lines[i++].Trim().Split(':')[1];
            BeatmapData.general.AudioLeadIn = int.Parse(lines[i++].Split(':')[1]);
            BeatmapData.general.PreviewTime = int.Parse(lines[i++].Split(':')[1]);
            BeatmapData.general.Countdown = int.Parse(lines[i++].Split(':')[1]);
            BeatmapData.general.SampleSet = lines[i++].Split(':')[1];
            BeatmapData.general.StackLeniency = float.Parse(lines[i++].Split(':')[1].Trim(), NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture);
            BeatmapData.general.Mode = int.Parse(lines[i++].Split(':')[1]);
            BeatmapData.general.LetterboxInBreaks = int.Parse(lines[i++].Split(':')[1]);
            BeatmapData.general.WidescreenStoryboard = int.Parse(lines[i++].Split(':')[1]);
        }

        private static void Editor(string[] lines)
        { 
            int i=1;
            if (lines[i].StartsWith("Bookmarks"))
            {
                BeatmapData.editor.Bookmarks = lines[i++].Split(':')[1].Split(',').ToList().ConvertAll(int.Parse);
            }
            BeatmapData.editor.DistanceSpacing = float.Parse(lines[i++].Split(':')[1].Trim(), NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture);
            BeatmapData.editor.BeatDivisor = int.Parse(lines[i++].Split(':')[1]);
            BeatmapData.editor.GridSize = int.Parse(lines[i++].Split(':')[1]);
            BeatmapData.editor.TimelineZoom = float.Parse(lines[i++].Split(':')[1].Trim(), NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture);
        }

        private static void Metadata(string[] lines)
        { 
            int i=1;
            BeatmapData.metadata.Title = lines[i++].Split(':')[1];
            BeatmapData.metadata.TitleUnicode = lines[i++].Split(':')[1];
            BeatmapData.metadata.Artist = lines[i++].Split(':')[1];
            BeatmapData.metadata.ArtistUnicode = lines[i++].Split(':')[1];
            BeatmapData.metadata.Creator = lines[i++].Split(':')[1];
            BeatmapData.metadata.Version = lines[i++].Split(':')[1];
            BeatmapData.metadata.Source = lines[i++].Split(':')[1];
            BeatmapData.metadata.Tags = lines[i++].Split(':')[1];
            BeatmapData.metadata.BeatmapID = int.Parse(lines[i++].Split(':')[1]);
            BeatmapData.metadata.BeatmapSetID = int.Parse(lines[i++].Split(':')[1]);
        }

        private static void Difficulty(string[] lines)
        { 
            int i=1;
            BeatmapData.difficulty.HPDrainRate = float.Parse(lines[i++].Split(':')[1].Trim(), NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture);
            BeatmapData.difficulty.CircleSize = float.Parse(lines[i++].Split(':')[1].Trim(), NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture);
            BeatmapData.difficulty.OverallDifficulty = float.Parse(lines[i++].Split(':')[1].Trim(), NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture);
            BeatmapData.difficulty.ApproachRate = float.Parse(lines[i++].Split(':')[1].Trim(), NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture);
            BeatmapData.difficulty.SliderMultiplier = float.Parse(lines[i++].Split(':')[1].Trim(), NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture);
            BeatmapData.difficulty.SliderTickrate = float.Parse(lines[i++].Split(':')[1].Trim(), NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture);
        }

        private static void Events(string[] lines)
        { 
            bool exitLoop = false;
            List<Action<string[]>> segments = new()
            {
                {(x)=>{}},
                {Background},
                {Breaks},
            }; 
            int current = 0;
            int i = 1;

            List<string> segment = new();
            for (i=1; i< lines.Length; i++)
            {
                if(exitLoop) break;
                string l = lines[i].Trim().TrimEnd('\r','\n');

                if (l.StartsWith("//"))
                {
                    segments[current++]([.. segment]);
                    segment.Clear();
                }
                else
                {
                    segment.Add(l);
                }
            }
            string[] sbDump = new string[lines.Length - i + 1];
            Array.Copy(lines, i-1, sbDump, 0, lines.Length - i + 1);
            BeatmapData.events.StoryboardLayerDump = string.Join( "", sbDump);

            void Background(string[] lines)
            {
                int i=0;
                string[] bg = lines[i++].Split(',');
                BeatmapData.events.SetBackground(bg[2], int.Parse(bg[3]), int.Parse(bg[3]));
                if(lines.Length > 1) 
                {
                    BeatmapData.events.SetVideo(lines[i].Split(',')[2], int.Parse(lines[i].Split(',')[1]));
                }
            }
            void Breaks(string[] lines)
            {
                for (int i=0; i < lines.Length; i++)
                {
                    string[] lb = lines[i].Split(',');
                    BeatmapData.events.AddBreakPeriod(int.Parse(lb[1]), int.Parse(lb[2]));
                }
                exitLoop = true;
            }
        }

        private static void TimingPoints(string[] lines)
        { 
            for (int i=1; i< lines.Length; i++)
            {
                string line = lines[i];
                string[] l = line.Split(',');
                int time = int.Parse(l[0]);
                float beatLength = float.Parse(l[1], NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture);
                int meter = int.Parse(l[2]);
                int sampleSet = int.Parse(l[3]);
                int sampleIndex = int.Parse(l[4]);
                int volume = int.Parse(l[5]);
                bool uninherited = int.Parse(l[6]) == 1;
                int effects = int.Parse(l[7]);
                TimingPoint t = new(time,beatLength,volume,uninherited,meter,sampleSet,sampleIndex,effects);

                if (!BeatmapData.timingPoints.ContainsKey(time))
                {
                    BeatmapData.timingPoints.Add(time, []);
                    BeatmapData.timingPoints[time].Add(t);
                }
                else
                {
                    BeatmapData.timingPoints[time].Add(t);
                }
            }
        }

        private static void Colours(string[] lines)
        { 
            for (int i=1; i< lines.Length; i++)
            {
                string line = lines[i].Replace(" ", "");
                string[] temp = line.Split(':');
                string[] l = temp[1].Split(',');

                int red = int.Parse(l[0]);
                int green = int.Parse(l[1]);
                int blue = int.Parse(l[2]);
                if (lines[i].StartsWith("SliderBorder"))
                {
                    BeatmapData.colours.Add(new Colour(red,green,blue,sliderBorder:true));
                    continue;
                }
                else if(lines[i].StartsWith("SliderTrackOverride"))
                {
                    BeatmapData.colours.Add(new Colour(red,green,blue,sliderTrackOverride:true));
                    continue;
                }
                BeatmapData.colours.Add(new Colour(red,green,blue));
            }
        }

        private static void HitObjects(string[] lines)
        { 
            for (int i=1; i< lines.Length; i++)
            {
                string line = lines[i];
                string[] l = line.Split(',');
                int x = int.Parse(l[0]);
                int y = int.Parse(l[1]);
                int time = int.Parse(l[2]);
                int type = int.Parse(l[3]);
                int hitSound = int.Parse(l[4]);

                string[] hitSampleLine = l[l.Length-1].Split(':');
                HitSample hitSample = new(int.Parse(hitSampleLine[0]),int.Parse(hitSampleLine[1]),
                            int.Parse(hitSampleLine[2]),int.Parse(hitSampleLine[3]),hitSampleLine[4]);

                HitObject o = null;
                switch (l.Length)
                {
                    //circle
                    case 6:
                        o = new(x,y,time,type,hitSound,hitSample);
                        break;

                    //spinner
                    case 7:
                        o = new(x, y, time, type, hitSound, hitSample, spinnerEndTime: int.Parse(l[5]));
                        break;

                    //slider
                    case 11:
                        string[] p = l[5].Split('|');
                        SliderParams.Curve curveType = (SliderParams.Curve)char.Parse(p[0]);

                        var points = p.ToList();
                        points.RemoveAt(0);
                        List<CurvePoint> curvePoints = points.ConvertAll(
                            new Converter<string, CurvePoint>((x) => 
                            { 
                                string[] coords = x.Split(':');
                                return new CurvePoint(int.Parse(coords[0]),int.Parse(coords[1]));
                            }));

                        int slides = int.Parse(l[6]);
                        float length = float.Parse(l[7], NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture);

                        List<int> edgeSounds = l[8].Split('|').ToList().ConvertAll(int.Parse);

                        List<string> edgeSets = l[9].Split('|').ToList();

                        SliderParams parameters = new SliderParams(curveType,curvePoints,slides,length,edgeSounds,edgeSets);

                        o = new(x,y,time,type,hitSound,hitSample,sliderParams:parameters);
                        break;
                }

                if (o == null) throw new Exception("Object added to HitObject while parsing file cannot be null.");
                if (!BeatmapData.hitObjects.ContainsKey(time))
                {
                    BeatmapData.hitObjects.Add(time, []);
                    BeatmapData.hitObjects[time].Add(o);
                }
                else
                {
                    BeatmapData.hitObjects[time].Add(o);
                }
            }
        }

    }
}
