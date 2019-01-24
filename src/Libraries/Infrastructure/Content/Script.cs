﻿// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Core
{
    public class Script
    {
        public string Url { get; set; }
        public int Position { get; }

        public Script(string url, int position)
        {
            this.Url = url;
            this.Position = position;
        }
    }
}