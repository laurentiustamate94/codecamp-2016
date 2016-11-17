﻿//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license.
//
// Microsoft Cognitive Services (formerly Project Oxford): https://www.microsoft.com/cognitive-services
//
// Microsoft Cognitive Services (formerly Project Oxford) GitHub:
// https://github.com/Microsoft/Cognitive-Vision-Windows
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

using System.IO;
using System.Threading.Tasks;

using Microsoft.ProjectOxford.Vision.Contract;
using System.Collections.Generic;
using System;

namespace Microsoft.ProjectOxford.Vision
{
    /// <summary>
    /// Vision service client interfaces.
    /// </summary>
    public interface IVisionServiceClient
    {
        /// <summary>
        /// Analyzes the image.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="visualFeatures">The visual features. If none are specified, 'Categories' will be analyzed.</param>
        /// <returns>The AnalysisResult object.</returns>
        Task<AnalysisResult> AnalyzeImageAsync(string url, string[] visualFeatures = null);

        /// <summary>
        /// Analyzes the image.
        /// </summary>
        /// <param name="imageStream">The image stream.</param>
        /// <param name="visualFeatures">The visual features. If none are specified, 'Categories' will be analyzed.</param>
        /// <returns>The AnalysisResult object.</returns>
        Task<AnalysisResult> AnalyzeImageAsync(Stream imageStream, string[] visualFeatures = null);

        /// <summary>
        /// Analyzes the image.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="visualFeatures">The visual features. If none are specified, VisualFeatures.Categories will be analyzed.</param>
        /// <param name="details">Optional domain-specific models to invoke when appropriate.  To obtain names of models supported, invoke the <see cref="ListModelsAsync">ListModelsAsync</see> method.</param>
        /// <returns>The AnalysisResult object.</returns>
        Task<AnalysisResult> AnalyzeImageAsync(string url, IEnumerable<VisualFeature> visualFeatures = null, IEnumerable<string> details = null);

        /// <summary>
        /// Analyzes the image.
        /// </summary>
        /// <param name="imageStream">The image stream.</param>
        /// <param name="visualFeatures">The visual features. If none are specified, VisualFeatures.Categories will be analyzed.</param>
        /// <param name="details">Optional domain-specific models to invoke when appropriate.  To obtain names of models supported, invoke the <see cref="ListModelsAsync">ListModelsAsync</see> method.</param>
        /// <returns>The AnalysisResult object.</returns>
        Task<AnalysisResult> AnalyzeImageAsync(Stream imageStream, IEnumerable<VisualFeature> visualFeatures = null, IEnumerable<string> details = null);

        /// <summary>
        /// Analyzes the image using a domain-specific image analysis model.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="model">Model representing the domain.</param>
        /// <returns>The AnalysisResult object.</returns>
        Task<AnalysisInDomainResult> AnalyzeImageInDomainAsync(string url, Model model);

        /// <summary>
        /// Analyzes the image using a domain-specific image analysis model.
        /// </summary>
        /// <param name="stream">The image stream.</param>
        /// <param name="visualFeatures">The visual features.</param>
        /// <returns>The AnalysisResult object.</returns>
        Task<AnalysisInDomainResult> AnalyzeImageInDomainAsync(Stream imageStream, Model model);

        /// <summary>
        /// Analyzes the image using a domain-specific image analysis model.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="modelName">Name of the model.</param>
        /// <returns>The AnalysisResult object.</returns>
        Task<AnalysisInDomainResult> AnalyzeImageInDomainAsync(string url, string modelName);

        /// <summary>
        /// Analyzes the image using a domain-specific image analysis model.
        /// </summary>
        /// <param name="stream">The image stream.</param>
        /// <param name="modelName">Name of the model.</param>
        /// <returns>The AnalysisResult object.</returns>
        Task<AnalysisInDomainResult> AnalyzeImageInDomainAsync(Stream imageStream, string modelName);

        /// <summary>
        /// List domain-specific models currently supproted.
        /// </summary>
        /// <returns></returns>
        Task<ModelResult> ListModelsAsync();

        /// <summary>
        /// List domain-specific models currently supproted.
        /// </summary>
        /// <returns></returns>
        Task<AnalysisResult> DescribeAsync(string url, int maxCandidates = 1);

        /// <summary>
        /// List domain-specific models currently supproted.
        /// </summary>
        /// <returns></returns>
        Task<AnalysisResult> DescribeAsync(Stream imageStream, int maxCandidates = 1);

        /// <summary>
        /// Gets the thumbnail.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="smartCropping">if set to <c>true</c> [smart cropping].</param>
        /// <returns>The byte array.</returns>
        Task<byte[]> GetThumbnailAsync(string url, int width, int height, bool smartCropping = true);

        /// <summary>
        /// Gets the thumbnail.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="smartCropping">if set to <c>true</c> [smart cropping].</param>
        /// <returns>The byte array.</returns>
        Task<byte[]> GetThumbnailAsync(Stream stream, int width, int height, bool smartCropping = true);

        /// <summary>
        /// Recognizes the text.
        /// </summary>
        /// <param name="imageUrl">The image URL.</param>
        /// <param name="languageCode">The language code.</param>
        /// <param name="detectOrientation">if set to <c>true</c> [detect orientation].</param>
        /// <returns>The OCR object.</returns>
        Task<OcrResults> RecognizeTextAsync(string imageUrl, string languageCode = LanguageCodes.AutoDetect, bool detectOrientation = true);

        /// <summary>
        /// Recognizes the text.
        /// </summary>
        /// <param name="imageStream">The image stream.</param>
        /// <param name="languageCode">The language code.</param>
        /// <param name="detectOrientation">if set to <c>true</c> [detect orientation].</param>
        /// <returns>The OCR object.</returns>
        Task<OcrResults> RecognizeTextAsync(Stream imageStream, string languageCode = LanguageCodes.AutoDetect, bool detectOrientation = true);

        /// <summary>
        /// Gets the tags associated with an image.
        /// </summary>
        /// <param name="imageUrl">The image URL.</param>
        /// <returns>Analysis result with tags.</returns>
        Task<AnalysisResult> GetTagsAsync(string imageUrl);

        /// <summary>
        /// Gets the tags associated with an image.
        /// </summary>
        /// <param name="imageStream">The image stream.</param>
        /// <returns>Analysis result with tags.</returns>
        Task<AnalysisResult> GetTagsAsync(Stream imageStream);
    }
}
