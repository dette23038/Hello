//-----------------------------------------------------------------------
// <copyright file="JsonLayout.cs" company="Ryan Woodcox">
//  Copyright (c) 2015 All Rights Reserved
//  <author>Ryan Woodcox</author>
// </copyright>
//-----------------------------------------------------------------------

namespace HelloWorldInfrastructure.Layouts
{
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using log4net.Core;
    using log4net.Layout;
    using Newtonsoft.Json;

    /// <summary>
    ///     Custom layout for Log4Net to format into a JSON string
    /// </summary>
    public class JsonLayout : LayoutSkeleton
    {
        /// <summary>
        ///     Overrides the Activate Options method for log4net's layout
        /// </summary>
        public override void ActivateOptions()
        {
        }

        /// <summary>
        ///     Overrides the Format method for log4net's layout
        /// </summary>
        /// <param name="writer">The text writer</param>
        /// <param name="loggingEvent">The logging event</param>
        public override void Format(TextWriter writer, LoggingEvent loggingEvent)
        {
            var dictionary = new Dictionary<string, object>();

            // Add the main properties
            dictionary.Add("timestamp", loggingEvent.TimeStamp);
            dictionary.Add("level", loggingEvent.Level != null ? loggingEvent.Level.DisplayName : "null");
            dictionary.Add("message", loggingEvent.RenderedMessage);
            dictionary.Add("logger", loggingEvent.LoggerName);

            // Loop through all other properties
            foreach (DictionaryEntry dictionaryEntry in loggingEvent.GetProperties())
            {
                var key = dictionaryEntry.Key.ToString();

                // Check if the key exists
                if (!dictionary.ContainsKey(key))
                {
                    dictionary.Add(key, dictionaryEntry.Value);
                }
            }

            // Convert the log string into a JSON string
            var logString = JsonConvert.SerializeObject(dictionary);

            writer.WriteLine(logString);
        }
    }
}
