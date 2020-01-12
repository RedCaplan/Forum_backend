using System.Text;

namespace Forum.Core.Extensions
{
    public static class StringExtensions
    {
        #region CyrillicTranslitiration

        static readonly string[] CyrillicToLatinL =
            "a,b,v,g,d,e,zh,z,i,j,k,l,m,n,o,p,r,s,t,u,f,kh,c,ch,sh,sch,j,y,j,e,yu,ya".Split(',');
        static readonly string[] CyrillicToLatinU =
            "A,B,V,G,D,E,Zh,Z,I,J,K,L,M,N,O,P,R,S,T,U,F,Kh,C,Ch,Sh,Sch,J,Y,J,E,Yu,Ya".Split(',');

        public static string CyrilicToLatin(this string s)
        {
            var sb = new StringBuilder((int)(s.Length * 1.5));
            foreach (char c in s)
            {
                if (c >= '\x430' && c <= '\x44f') sb.Append(CyrillicToLatinL[c - '\x430']);
                else if (c >= '\x410' && c <= '\x42f') sb.Append(CyrillicToLatinU[c - '\x410']);
                else if (c == '\x401') sb.Append("Yo");
                else if (c == '\x451') sb.Append("yo");
                else if (c == '\x20') sb.Append('-');
                else sb.Append(c);
            }

            return sb.ToString().ToLower();
        }

        #endregion
    }
}
