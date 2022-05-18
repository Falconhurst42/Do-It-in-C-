using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;

namespace DoItInCpp.Utilities
{
    public static class Parsers
    {
        public class ParsedDescription {
            public List<Block> blocks { get; set; }

            public ParsedDescription(String desc) {
                blocks = new List<Block>();
                // split into code and text blocks
                var chunks = desc.Split("```");
                bool isCode = false;
                if (chunks[0] == "")
                    isCode = true;
                for(int i = isCode ? 1 : 0; i < chunks.Length; i++) {
                    if(isCode = !isCode)
                        blocks.Add(new TextBlock(chunks[i]));
                    else
                        blocks.Add(new CodeBlock(chunks[i]));
                }
            }
        }

        public class Block {

        }
        public class CodeBlock : Block {
            public String contents { get; set; }
            public CodeBlock(String c) {contents = c;}
        }

        // <a, "text text text](cplusplus.com"> -> <a href="cplusplus.com" target="_blank">text text text</a>
        public class Line {
            public enum Tag {p, code, a};
            public List<KeyValuePair<Tag, String>> contents {get; set;}

            public Line(String line) {
                contents = new List<KeyValuePair<Tag, String>>();
                // insert <code> blocks given ` sets
                bool isCode = false;
                int i = 0, next = line.IndexOf('`', 0);
                string l = "";
                while(next != -1) {
                    l = line.Substring(i, next-i);
                    // removed spaces in code to remove spaces in output
                    contents.Add(new KeyValuePair<Tag, String>(isCode ? Tag.code : Tag.p, l));
                    i = next+1;
                    next = line.IndexOf('`', i);
                    isCode = !isCode;
                }
                if(i != line.Length - 1)
                    contents.Add(new KeyValuePair<Tag, String>(Tag.p, line.Substring(i)));
                
                // extract <a> blocks from <p> blocks
                for(int li = 0; li < contents.Count; li++) {
                    KeyValuePair<Tag, String> pair = contents[li];
                    if(pair.Key == Tag.p) {
                        next = pair.Value.IndexOf("](", 0);
                        if(next != -1) {
                            int first = pair.Value.LastIndexOf("[", next);
                            int last = pair.Value.IndexOf(")", next);
                            contents[li] = new KeyValuePair<Tag, String>(Tag.p, pair.Value.Substring(0, first));
                            contents.Insert(li+1, new KeyValuePair<Tag, String>(Tag.a, pair.Value.Substring(first+1, last-first-1)));
                            contents.Insert(li+2, new KeyValuePair<Tag, String>(Tag.p, pair.Value.Substring(last+1)));
                        }
                    }
                }
            }
        }

        public class TextBlock : Block {
            // cont : [line, line, line]
            // line: [text? code? text?]
            public List<Line> contents { get; set; }

            public TextBlock(String conts) {
                contents = new List<Line>();
                foreach (var line in conts.Split('\n'))
                {
                    contents.Add(new Line(line));
                }
            }
        }
    }
}