using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Toneitor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests {

    public class TestScript {
        [Test]
        public void LoadTonesTest() {
            ToneController toneController = new ToneController();
            Tone[] tones = toneController.LoadTones();
            Assert.NotNull(tones);
            Assert.Greater(tones.Length, 0);
        }

        [Test]
        [TestCase("A", 27.5f)]
        public void GetToneTest(string toneName, float frequency) {
            ToneController toneController = new ToneController();
            Tone tone = toneController.GetTone("A");
            Assert.NotNull(tone);
            Assert.AreEqual(frequency, tone.BaseFrequency);
        }

        [Test]
        [TestCase("A", 4, 440)]
        public void OctaveToneTest(string toneName, int octave, float frequency) {
            ToneController toneController = new ToneController();
            Tone tone = toneController.GetTone("A");
            OctaveTone octaveTone = new OctaveTone(tone, octave);
            Assert.AreEqual(toneName, octaveTone.ToneName);
            Assert.AreEqual(octave, octaveTone.Octave);
            Assert.AreEqual(toneName + octave, octaveTone.Name);
            Assert.AreEqual(tone.BaseFrequency * Mathf.Pow(2, octave), octaveTone.Frequency);
            Assert.AreEqual(frequency, octaveTone.Frequency);
        }

        [Test]
        public void GetRandomToneTest() {
            ToneController toneController = new ToneController();
            Tone tone = toneController.GetRandomTone();
            Assert.NotNull(tone);
            Assert.Greater(tone.BaseFrequency, 0);
        }

        [Test]
        [TestCase("A", 2)]
        public void GetOctaveToneTest(string note, int octave) {
            ToneController toneController = new ToneController();
            OctaveTone octaveTone = toneController.GetOctaveTone(note, octave);
            OctaveTone octaveToneBase = toneController.GetOctaveTone(note, 0);
            Assert.NotNull(octaveTone);
            Assert.AreEqual(octaveTone.Name, note + octave);

        }

        [Test]
        public void GetRandomOctaveToneTest() {
            ToneController toneController = new ToneController();
            OctaveTone octaveTone = toneController.GetRandomOctaveTone();
            Assert.NotNull(octaveTone);
        }

        [Test]
        [TestCase(3)]
        public void GetRandomOctaveToneTest(int octave) {
            ToneController toneController = new ToneController();
            OctaveTone octaveTone = toneController.GetRandomOctaveTone(octave);
            Assert.NotNull(octaveTone);
        }

        private static readonly object[] _majorScalesCases =
    {
        new object[] {"C",2,8,new List<string> {"C2", "D2", "E2", "F2", "G2", "A2", "B2", "C3" } },   //case 1
        new object[] { "G", 2, 8, new List<string> { "G2", "A2", "B2", "C3", "D3", "E3", "F#3", "G3" } } //case 2
    };

        private static readonly object[] _minorScalesCases =
    {
        new object[] {"A",2,8,new List<string> { "A2", "B2", "C3", "D3", "E3", "F3", "G3", "A3"} },   //case 1
        new object[] { "C", 2, 8, new List<string> { "C2", "D2", "D#2", "F2", "G2", "G#2", "A#2", "C3" } } //case 2
    };

        [Test]
        [TestCaseSource(nameof(_majorScalesCases))]
        public void GetMajorScaleTest(string note,int octave, int count,List<string> expected) {
            ToneController toneController = new ToneController();
            OctaveTone tone = toneController.GetOctaveTone(note, octave);
            List<OctaveTone> scale = toneController.GetScale(tone, count, ToneController.MajorScale);
            Assert.NotNull(scale);
            Assert.IsNotEmpty(scale);
            Assert.AreEqual(count,scale.Count);
            for (int i = 0; i < count; i++) {
                Assert.AreEqual(expected[i], scale[i].Name);                
            }
            
        }

        [Test]
        [TestCaseSource(nameof(_minorScalesCases))]
        public void GetMinorScaleTest(string note, int octave, int count, List<string> expected) {
            ToneController toneController = new ToneController();
            OctaveTone tone = toneController.GetOctaveTone(note, octave);
            List<OctaveTone> scale = toneController.GetScale(tone, count, ToneController.NaturalMinorScale);
            Assert.NotNull(scale);
            Assert.IsNotEmpty(scale);
            Assert.AreEqual(count, scale.Count);
            for (int i = 0; i < count; i++) {
                Assert.AreEqual(expected[i], scale[i].Name);
            }

        }

        [Test]
        [TestCase("C", 2,"C#2")]
        [TestCase("E", 2,"F2")]
        [TestCase("G#", 2,"A2")]
        [TestCase("A", 3,"A#3")]
        [TestCase("B", 3,"C4")]
        public void GetNextSemiToneTest(string note, int octave, string semiToneName) {
            ToneController toneController = new ToneController();
            OctaveTone tone = toneController.GetOctaveTone(note,octave);
            OctaveTone semiTone = toneController.GetNextSemiTone(tone);
            Assert.NotNull(semiTone);
            Assert.AreEqual(semiTone.Name, semiToneName);
        }
    }
}