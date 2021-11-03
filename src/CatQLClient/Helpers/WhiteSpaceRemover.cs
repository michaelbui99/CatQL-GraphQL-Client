using System;
using System.Linq;

namespace CatQL.GraphQL.Helpers
{
    public static class WhiteSpaceRemover
    {
        public static string RemoveWhiteSpace(string s)
        {
            return new string(s.ToCharArray().Where((c)=> !Char.IsWhiteSpace(c)).ToArray()); 
        }
    }
}