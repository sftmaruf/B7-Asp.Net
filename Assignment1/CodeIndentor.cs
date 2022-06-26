using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    public class CodeIndentor : ICodeIndentor
    {
        public StringBuilder Indent(StringBuilder _json)
        {
            RemoveExtraCommas(ref _json);
            RemoveExtraSpace(ref _json);
            IndentJson(ref _json);
            return _json;
        }

        public void RemoveExtraCommas(ref StringBuilder _json)
        {
            int commaPosition = -1;
            for (int i = 0; i < _json.Length; i++)
            {
                if (_json[i] == ',') commaPosition = i;
                if (isUneligibleToBecomeExtraComma(commaPosition, _json[i])) commaPosition = -1;
                if (isExtraComma(commaPosition, _json[i]))
                {
                    _json = _json.Remove(commaPosition, 1);
                    i--;
                }
            }
        }

        private bool isUneligibleToBecomeExtraComma(in int commaPosition, char character)
        {
            return commaPosition != -1 && (character != ' ' && character != '}' && character != ']' && character != ',' && character != '\n' && character != '\t');
        }

        private bool isExtraComma(in int commaPosition, char character)
        {
            return commaPosition != -1 && (character == '}' || character == ']');
        }

        public void RemoveExtraSpace(ref StringBuilder _json)
        {
            int spacePosition = -1, openThirdBracketPosition = -1;
            for (int i = 0; i < _json.Length; i++)
            {
                if (_json[i] == ' ') spacePosition = i;
                if (_json[i] == '[') openThirdBracketPosition = i;

                int position = determineExtraSpace(ref spacePosition, ref openThirdBracketPosition, _json[i]);
                if (position != -1)
                {
                    _json = _json.Remove(position, 1);
                    i--;
                }
            }
        }

        private int determineExtraSpace(ref int spacePosition, ref int openThirdBracketPosition, char character)
        {
            if (spacePosition != -1 && character != ' ' && character != ']') spacePosition = -1;
            if (spacePosition != -1 && character == ']')
            {
                return spacePosition;
            }

            if (openThirdBracketPosition != -1 && character != ' ' && character != '[') openThirdBracketPosition = -1;
            if (openThirdBracketPosition != -1 && character == ' ')
            {
                return openThirdBracketPosition + 1;
            }
            return -1;
        }

        public void IndentJson(ref StringBuilder _json)
        {
            bool isColon = false, isColon1 = false, isClosedBracket = false, isComma = false, isString = false;
            StringBuilder sb = new StringBuilder().Append('\t');

            walk(0, _json);
            void walk(int i, StringBuilder _json)
            {
                if (i < _json.Length - 2)
                {
                    if (_json[i] == '"') isString = true;
                    if (isString && _json[i] == '"') isString = false;

                    if (isObjectStart(_json[i], isString, ref isColon, ref isComma)) sb.Append('\t');
                    if (isObjectClosed(_json[i], ref isClosedBracket, ref isColon1)) sb.Remove(0, 1);
                    if (isNewLine(_json[i])) _json.Insert(i + 1, sb);
                    walk(i + 1, _json);
                }
            }
        }

        private bool isObjectStart(in char character, in bool isString, ref bool isColon, ref bool isComma)
        {
            if (character == ':') isColon = true;
            if (isColon)
            {
                if (character != ' ' && character != '{' && character != ':'
                    && character != '[' && character != '\n' && character != '\t') isColon = false;

                if (!isString && character == '{')
                {
                    return true;
                }
            }

            if (character == ',') isComma = true;
            if (isComma)
            {
                if (character != ' ' && character != ',' && character != '{') isComma = false;
                if (!isString && character == '{') return true;
            }
            return false;
        }

        private bool isObjectClosed(in char character, ref bool isClosedBracket, ref bool isColon)
        {
            if (character == '}') isClosedBracket = true;
            if (isClosedBracket)
            {
                if (character != '\n' && character != '}' && character != ' ' && character != ']') isClosedBracket = false;
                if (character == '\n')
                {
                    return true;
                }
            }

            if (character == ':') isColon = true;
            if (isColon)
            {
                if (character == ',' || character == '{' || character == '[') isColon = false;
                if (character == '\n')
                {
                    isColon = false;
                    return true;
                }
            }

            return false;
        }

        private bool isNewLine(in char character)
        {
            return character == '\n';
        }
    }
}
