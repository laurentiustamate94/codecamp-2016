﻿// 
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license.
// 
// Microsoft Cognitive Services (formerly Project Oxford): https://www.microsoft.com/cognitive-services
// 
// Microsoft Cognitive Services (formerly Project Oxford) GitHub:
// https://github.com/Microsoft/Cognitive-Emotion-Windows
// 
// Copyright (c) Microsoft Corporation
// All rights reserved.
// 
// MIT License:
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED ""AS IS"", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// 

using System.Collections.Generic;
using System.Linq;

namespace Microsoft.ProjectOxford.Emotion.Contract
{
    ///
    public class Scores
    {
        /// <summary>
        /// 
        /// </summary>
        public float Anger { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public float Contempt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public float Disgust { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public float Fear { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public float Happiness { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public float Neutral { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public float Sadness { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public float Surprise { get; set; }

        /// <summary>
        /// Create a sorted key-value pair of emotions and the corresponding scores, sorted from highest score on down.
        /// To make the ordering stable, the score is the primary key, and the name is the secondary key.
        /// </summary>
        public IEnumerable<KeyValuePair<string, float>> ToRankedList()
        {
            return new Dictionary<string, float>()
            {
                { "Anger", Anger },
                { "Contempt", Contempt },
                { "Disgust", Disgust },
                { "Fear", Fear },
                { "Happiness", Happiness },
                { "Neutral", Neutral },
                { "Sadness", Sadness },
                { "Surprise", Surprise }
            }
            .OrderByDescending(kv => kv.Value)
            .ThenBy(kv => kv.Key)
            .ToList();
        }

        #region overrides
        public override bool Equals(object o)
        {
            if (o == null) return false;

            var other = o as Scores;
            if (other == null) return false;

            return this.Anger == other.Anger &&
                this.Disgust == other.Disgust &&
                this.Fear == other.Fear &&
                this.Happiness == other.Happiness &&
                this.Neutral == other.Neutral &&
                this.Sadness == other.Sadness &&
                this.Surprise == other.Surprise;
        }

        public override int GetHashCode()
        {
            return Anger.GetHashCode() ^
                Disgust.GetHashCode() ^
                Fear.GetHashCode() ^
                Happiness.GetHashCode() ^
                Neutral.GetHashCode() ^
                Sadness.GetHashCode() ^
                Surprise.GetHashCode();
        }
        #endregion;
    }
}
