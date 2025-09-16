using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.VisualBasic.FileIO;

namespace IOLib
{
    public class CSVFileParser_ModifiedVB
    {
    }
}


#region Assembly Microsoft.VisualBasic, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\Microsoft.VisualBasic.dll
// Decompiled with ICSharpCode.Decompiler 8.2.0.7535
#endregion



//
// Summary:
//     Provides methods and properties for parsing structured text files.
public class TextFieldParser : IDisposable
{
    private delegate int ChangeBufferFunction();

    private bool m_Disposed;

    private TextReader m_Reader;

    private string[] m_CommentTokens;

    private long m_LineNumber;

    private bool m_EndOfData;

    private string m_ErrorLine;

    private long m_ErrorLineNumber;

    private FieldType m_TextFieldType;

    private int[] m_FieldWidths;

    private string[] m_Delimiters;

    private int[] m_FieldWidthsCopy;

    private string[] m_DelimitersCopy;

    private Regex m_DelimiterRegex;

    private Regex m_DelimiterWithEndCharsRegex;

    private const RegexOptions REGEX_OPTIONS = RegexOptions.CultureInvariant;

    private int[] m_WhitespaceCodes;

    private Regex m_BeginQuotesRegex;

    private Regex m_WhiteSpaceRegEx;

    private bool m_TrimWhiteSpace;

    private int m_Position;

    private int m_PeekPosition;

    private int m_CharsRead;

    private bool m_NeedPropertyCheck;

    private const int DEFAULT_BUFFER_LENGTH = 4096;

    private const int DEFAULT_BUILDER_INCREASE = 10;

    private char[] m_Buffer;

    private int m_LineLength;

    private bool m_HasFieldsEnclosedInQuotes;

    private string m_SpaceChars;

    private int m_MaxLineSize;

    private int m_MaxBufferSize;

    private const string BEGINS_WITH_QUOTE = "\\G[{0}]*\"";

    private const string ENDING_QUOTE = "\"[{0}]*";

    private bool m_LeaveOpen;

    //
    // Summary:
    //     Defines comment tokens. A comment token is a string that, when placed at the
    //     beginning of a line, indicates that the line is a comment and should be ignored
    //     by the parser.
    //
    // Returns:
    //     A string array that contains all of the comment tokens for the Microsoft.VisualBasic.FileIO.TextFieldParser
    //     object.
    //
    // Exceptions:
    //   T:System.ArgumentException:
    //     A comment token includes white space.
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public string[] CommentTokens
    {
        get
        {
            return m_CommentTokens;
        }
        set
        {
            CheckCommentTokensForWhitespace(value);
            m_CommentTokens = value;
            m_NeedPropertyCheck = true;
        }
    }

    //
    // Summary:
    //     Returns True if there are no non-blank, non-comment lines between the current
    //     cursor position and the end of the file.
    //
    // Returns:
    //     True if there is no more data to read; otherwise, False.
    public bool EndOfData
    {
        get
        {
            if (m_EndOfData)
            {
                return m_EndOfData;
            }

            if ((m_Reader == null) | (m_Buffer == null))
            {
                m_EndOfData = true;
                return true;
            }

            if (PeekNextDataLine() != null)
            {
                return false;
            }

            m_EndOfData = true;
            return true;
        }
    }

    //
    // Summary:
    //     Returns the current line number, or returns -1 if no more characters are available
    //     in the stream.
    //
    // Returns:
    //     The current line number.
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public long LineNumber
    {
        get
        {
            if (m_LineNumber != -1 && ((m_Reader.Peek() == -1) & (m_Position == m_CharsRead)))
            {
                CloseReader();
            }

            return m_LineNumber;
        }
    }

    //
    // Summary:
    //     Returns the line that caused the most recent Microsoft.VisualBasic.FileIO.MalformedLineException
    //     exception.
    //
    // Returns:
    //     The line that caused the most recent Microsoft.VisualBasic.FileIO.MalformedLineException
    //     exception.
    public string ErrorLine => m_ErrorLine;

    //
    // Summary:
    //     Returns the number of the line that caused the most recent Microsoft.VisualBasic.FileIO.MalformedLineException
    //     exception.
    //
    // Returns:
    //     The number of the line that caused the most recent Microsoft.VisualBasic.FileIO.MalformedLineException
    //     exception.
    public long ErrorLineNumber => m_ErrorLineNumber;

    //
    // Summary:
    //     Indicates whether the file to be parsed is delimited or fixed-width.
    //
    // Returns:
    //     A Microsoft.VisualBasic.FileIO.TextFieldParser.TextFieldType value that indicates
    //     whether the file to be parsed is delimited or fixed-width.
    public FieldType TextFieldType
    {
        get
        {
            return m_TextFieldType;
        }
        set
        {
            ValidateFieldTypeEnumValue(value, "value");
            m_TextFieldType = value;
            m_NeedPropertyCheck = true;
        }
    }

    //
    // Summary:
    //     Denotes the width of each column in the text file being parsed.
    //
    // Returns:
    //     An integer array that contains the width of each column in the text file that
    //     is being parsed.
    //
    // Exceptions:
    //   T:System.ArgumentException:
    //     A width value in any location other than the last entry of the array is less
    //     than or equal to zero.
    public int[] FieldWidths
    {
        get
        {
            return m_FieldWidths;
        }
        set
        {
            if (value != null)
            {
                ValidateFieldWidthsOnInput(value);
                m_FieldWidthsCopy = (int[])value.Clone();
            }
            else
            {
                m_FieldWidthsCopy = null;
            }

            m_FieldWidths = value;
            m_NeedPropertyCheck = true;
        }
    }

    //
    // Summary:
    //     Defines the delimiters for a text file.
    //
    // Returns:
    //     A string array that contains all of the field delimiters for the Microsoft.VisualBasic.FileIO.TextFieldParser
    //     object.
    //
    // Exceptions:
    //   T:System.ArgumentException:
    //     A delimiter value is set to a newline character, an empty string, or Nothing.
    public string[] Delimiters
    {
        get
        {
            return m_Delimiters;
        }
        set
        {
            if (value != null)
            {
                ValidateDelimiters(value);
                m_DelimitersCopy = (string[])value.Clone();
            }
            else
            {
                m_DelimitersCopy = null;
            }

            m_Delimiters = value;
            m_NeedPropertyCheck = true;
            m_BeginQuotesRegex = null;
        }
    }

    //
    // Summary:
    //     Indicates whether leading and trailing white space should be trimmed from field
    //     values.
    //
    // Returns:
    //     True if leading and trailing white space should be trimmed from field values;
    //     otherwise, False.
    public bool TrimWhiteSpace
    {
        get
        {
            return m_TrimWhiteSpace;
        }
        set
        {
            m_TrimWhiteSpace = value;
        }
    }

    //
    // Summary:
    //     Denotes whether fields are enclosed in quotation marks when a delimited file
    //     is being parsed.
    //
    // Returns:
    //     True if fields are enclosed in quotation marks; otherwise, False.
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public bool HasFieldsEnclosedInQuotes
    {
        get
        {
            return m_HasFieldsEnclosedInQuotes;
        }
        set
        {
            m_HasFieldsEnclosedInQuotes = value;
        }
    }

    private Regex BeginQuotesRegex
    {
        get
        {
            if (m_BeginQuotesRegex == null)
            {
                string pattern = string.Format(CultureInfo.InvariantCulture, "\\G[{0}]*\"", new object[1] { WhitespacePattern });
                m_BeginQuotesRegex = new Regex(pattern, RegexOptions.CultureInvariant);
            }

            return m_BeginQuotesRegex;
        }
    }

    private string EndQuotePattern => string.Format(CultureInfo.InvariantCulture, "\"[{0}]*", new object[1] { WhitespacePattern });

    private string WhitespaceCharacters
    {
        get
        {
            StringBuilder stringBuilder = new StringBuilder();
            int[] whitespaceCodes = m_WhitespaceCodes;
            for (int i = 0; i < whitespaceCodes.Length; i = checked(i + 1))
            {
                char c = Strings.ChrW(whitespaceCodes[i]);
                if (!CharacterIsInDelimiter(c))
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString();
        }
    }

    private string WhitespacePattern
    {
        get
        {
            StringBuilder stringBuilder = new StringBuilder();
            int[] whitespaceCodes = m_WhitespaceCodes;
            for (int i = 0; i < whitespaceCodes.Length; i = checked(i + 1))
            {
                int charCode = whitespaceCodes[i];
                char testCharacter = Strings.ChrW(charCode);
                if (!CharacterIsInDelimiter(testCharacter))
                {
                    stringBuilder.Append("\\u" + charCode.ToString("X4", CultureInfo.InvariantCulture));
                }
            }

            return stringBuilder.ToString();
        }
    }

    //
    // Summary:
    //     Initializes a new instance of the TextFieldParser class.
    //
    // Parameters:
    //   path:
    //     String. The complete path of the file to be parsed.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     path is an empty string.
    [HostProtection(SecurityAction.LinkDemand, Resources = HostProtectionResource.ExternalProcessMgmt)]
    public TextFieldParser(string path)
    {
        m_CommentTokens = new string[0];
        m_LineNumber = 1L;
        m_EndOfData = false;
        m_ErrorLine = "";
        m_ErrorLineNumber = -1L;
        m_TextFieldType = FieldType.Delimited;
        m_WhitespaceCodes = new int[23]
        {
            9, 11, 12, 32, 133, 160, 5760, 8192, 8193, 8194,
            8195, 8196, 8197, 8198, 8199, 8200, 8201, 8202, 8203, 8232,
            8233, 12288, 65279
        };
        m_WhiteSpaceRegEx = new Regex("\\s", RegexOptions.CultureInvariant);
        m_TrimWhiteSpace = true;
        m_Position = 0;
        m_PeekPosition = 0;
        m_CharsRead = 0;
        m_NeedPropertyCheck = true;
        m_Buffer = new char[4096];
        m_HasFieldsEnclosedInQuotes = true;
        m_MaxLineSize = 10000000;
        m_MaxBufferSize = 10000000;
        m_LeaveOpen = false;
        InitializeFromPath(path, Encoding.UTF8, detectEncoding: true);
    }

    //
    // Summary:
    //     Initializes a new instance of the TextFieldParser class.
    //
    // Parameters:
    //   path:
    //     String. The complete path of the file to be parsed.
    //
    //   defaultEncoding:
    //     System.Text.Encoding. The character encoding to use if encoding is not determined
    //     from file. Default is System.Text.Encoding.UTF8.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     path is an empty string or defaultEncoding is Nothing.
    [HostProtection(SecurityAction.LinkDemand, Resources = HostProtectionResource.ExternalProcessMgmt)]
    public TextFieldParser(string path, Encoding defaultEncoding)
    {
        m_CommentTokens = new string[0];
        m_LineNumber = 1L;
        m_EndOfData = false;
        m_ErrorLine = "";
        m_ErrorLineNumber = -1L;
        m_TextFieldType = FieldType.Delimited;
        m_WhitespaceCodes = new int[23]
        {
            9, 11, 12, 32, 133, 160, 5760, 8192, 8193, 8194,
            8195, 8196, 8197, 8198, 8199, 8200, 8201, 8202, 8203, 8232,
            8233, 12288, 65279
        };
        m_WhiteSpaceRegEx = new Regex("\\s", RegexOptions.CultureInvariant);
        m_TrimWhiteSpace = true;
        m_Position = 0;
        m_PeekPosition = 0;
        m_CharsRead = 0;
        m_NeedPropertyCheck = true;
        m_Buffer = new char[4096];
        m_HasFieldsEnclosedInQuotes = true;
        m_MaxLineSize = 10000000;
        m_MaxBufferSize = 10000000;
        m_LeaveOpen = false;
        InitializeFromPath(path, defaultEncoding, detectEncoding: true);
    }

    //
    // Summary:
    //     Initializes a new instance of the TextFieldParser class.
    //
    // Parameters:
    //   path:
    //     String. The complete path of the file to be parsed.
    //
    //   defaultEncoding:
    //     System.Text.Encoding. The character encoding to use if encoding is not determined
    //     from file. Default is System.Text.Encoding.UTF8.
    //
    //   detectEncoding:
    //     Boolean. Indicates whether to look for byte order marks at the beginning of the
    //     file. Default is True.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     path is an empty string or defaultEncoding is Nothing.
    [HostProtection(SecurityAction.LinkDemand, Resources = HostProtectionResource.ExternalProcessMgmt)]
    public TextFieldParser(string path, Encoding defaultEncoding, bool detectEncoding)
    {
        m_CommentTokens = new string[0];
        m_LineNumber = 1L;
        m_EndOfData = false;
        m_ErrorLine = "";
        m_ErrorLineNumber = -1L;
        m_TextFieldType = FieldType.Delimited;
        m_WhitespaceCodes = new int[23]
        {
            9, 11, 12, 32, 133, 160, 5760, 8192, 8193, 8194,
            8195, 8196, 8197, 8198, 8199, 8200, 8201, 8202, 8203, 8232,
            8233, 12288, 65279
        };
        m_WhiteSpaceRegEx = new Regex("\\s", RegexOptions.CultureInvariant);
        m_TrimWhiteSpace = true;
        m_Position = 0;
        m_PeekPosition = 0;
        m_CharsRead = 0;
        m_NeedPropertyCheck = true;
        m_Buffer = new char[4096];
        m_HasFieldsEnclosedInQuotes = true;
        m_MaxLineSize = 10000000;
        m_MaxBufferSize = 10000000;
        m_LeaveOpen = false;
        InitializeFromPath(path, defaultEncoding, detectEncoding);
    }

    //
    // Summary:
    //     Initializes a new instance of the TextFieldParser class.
    //
    // Parameters:
    //   stream:
    //     System.IO.Stream. The stream to be parsed.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     stream is Nothing.
    //
    //   T:System.ArgumentException:
    //     stream cannot be read from.
    [HostProtection(SecurityAction.LinkDemand, Resources = HostProtectionResource.ExternalProcessMgmt)]
    public TextFieldParser(Stream stream)
    {
        m_CommentTokens = new string[0];
        m_LineNumber = 1L;
        m_EndOfData = false;
        m_ErrorLine = "";
        m_ErrorLineNumber = -1L;
        m_TextFieldType = FieldType.Delimited;
        m_WhitespaceCodes = new int[23]
        {
            9, 11, 12, 32, 133, 160, 5760, 8192, 8193, 8194,
            8195, 8196, 8197, 8198, 8199, 8200, 8201, 8202, 8203, 8232,
            8233, 12288, 65279
        };
        m_WhiteSpaceRegEx = new Regex("\\s", RegexOptions.CultureInvariant);
        m_TrimWhiteSpace = true;
        m_Position = 0;
        m_PeekPosition = 0;
        m_CharsRead = 0;
        m_NeedPropertyCheck = true;
        m_Buffer = new char[4096];
        m_HasFieldsEnclosedInQuotes = true;
        m_MaxLineSize = 10000000;
        m_MaxBufferSize = 10000000;
        m_LeaveOpen = false;
        InitializeFromStream(stream, Encoding.UTF8, detectEncoding: true);
    }

    //
    // Summary:
    //     Initializes a new instance of the TextFieldParser class.
    //
    // Parameters:
    //   stream:
    //     System.IO.Stream. The stream to be parsed.
    //
    //   defaultEncoding:
    //     System.Text.Encoding. The character encoding to use if encoding is not determined
    //     from file. Default is System.Text.Encoding.UTF8.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     stream or defaultEncoding is Nothing.
    //
    //   T:System.ArgumentException:
    //     stream cannot be read from.
    [HostProtection(SecurityAction.LinkDemand, Resources = HostProtectionResource.ExternalProcessMgmt)]
    public TextFieldParser(Stream stream, Encoding defaultEncoding)
    {
        m_CommentTokens = new string[0];
        m_LineNumber = 1L;
        m_EndOfData = false;
        m_ErrorLine = "";
        m_ErrorLineNumber = -1L;
        m_TextFieldType = FieldType.Delimited;
        m_WhitespaceCodes = new int[23]
        {
            9, 11, 12, 32, 133, 160, 5760, 8192, 8193, 8194,
            8195, 8196, 8197, 8198, 8199, 8200, 8201, 8202, 8203, 8232,
            8233, 12288, 65279
        };
        m_WhiteSpaceRegEx = new Regex("\\s", RegexOptions.CultureInvariant);
        m_TrimWhiteSpace = true;
        m_Position = 0;
        m_PeekPosition = 0;
        m_CharsRead = 0;
        m_NeedPropertyCheck = true;
        m_Buffer = new char[4096];
        m_HasFieldsEnclosedInQuotes = true;
        m_MaxLineSize = 10000000;
        m_MaxBufferSize = 10000000;
        m_LeaveOpen = false;
        InitializeFromStream(stream, defaultEncoding, detectEncoding: true);
    }

    //
    // Summary:
    //     Initializes a new instance of the TextFieldParser class.
    //
    // Parameters:
    //   stream:
    //     System.IO.Stream. The stream to be parsed.
    //
    //   defaultEncoding:
    //     System.Text.Encoding. The character encoding to use if encoding is not determined
    //     from file. Default is System.Text.Encoding.UTF8.
    //
    //   detectEncoding:
    //     Boolean. Indicates whether to look for byte order marks at the beginning of the
    //     file. Default is True.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     stream or defaultEncoding is Nothing.
    //
    //   T:System.ArgumentException:
    //     stream cannot be read from.
    [HostProtection(SecurityAction.LinkDemand, Resources = HostProtectionResource.ExternalProcessMgmt)]
    public TextFieldParser(Stream stream, Encoding defaultEncoding, bool detectEncoding)
    {
        m_CommentTokens = new string[0];
        m_LineNumber = 1L;
        m_EndOfData = false;
        m_ErrorLine = "";
        m_ErrorLineNumber = -1L;
        m_TextFieldType = FieldType.Delimited;
        m_WhitespaceCodes = new int[23]
        {
            9, 11, 12, 32, 133, 160, 5760, 8192, 8193, 8194,
            8195, 8196, 8197, 8198, 8199, 8200, 8201, 8202, 8203, 8232,
            8233, 12288, 65279
        };
        m_WhiteSpaceRegEx = new Regex("\\s", RegexOptions.CultureInvariant);
        m_TrimWhiteSpace = true;
        m_Position = 0;
        m_PeekPosition = 0;
        m_CharsRead = 0;
        m_NeedPropertyCheck = true;
        m_Buffer = new char[4096];
        m_HasFieldsEnclosedInQuotes = true;
        m_MaxLineSize = 10000000;
        m_MaxBufferSize = 10000000;
        m_LeaveOpen = false;
        InitializeFromStream(stream, defaultEncoding, detectEncoding);
    }

    //
    // Summary:
    //     Initializes a new instance of the TextFieldParser class.
    //
    // Parameters:
    //   stream:
    //     System.IO.Stream. The stream to be parsed.
    //
    //   defaultEncoding:
    //     System.Text.Encoding. The character encoding to use if encoding is not determined
    //     from file. Default is System.Text.Encoding.UTF8.
    //
    //   detectEncoding:
    //     Boolean. Indicates whether to look for byte order marks at the beginning of the
    //     file. Default is True.
    //
    //   leaveOpen:
    //     Boolean. Indicates whether to leave stream open when the TextFieldParser object
    //     is closed. Default is False.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     stream or defaultEncoding is Nothing.
    //
    //   T:System.ArgumentException:
    //     stream cannot be read from.
    [HostProtection(SecurityAction.LinkDemand, Resources = HostProtectionResource.ExternalProcessMgmt)]
    public TextFieldParser(Stream stream, Encoding defaultEncoding, bool detectEncoding, bool leaveOpen)
    {
        m_CommentTokens = new string[0];
        m_LineNumber = 1L;
        m_EndOfData = false;
        m_ErrorLine = "";
        m_ErrorLineNumber = -1L;
        m_TextFieldType = FieldType.Delimited;
        m_WhitespaceCodes = new int[23]
        {
            9, 11, 12, 32, 133, 160, 5760, 8192, 8193, 8194,
            8195, 8196, 8197, 8198, 8199, 8200, 8201, 8202, 8203, 8232,
            8233, 12288, 65279
        };
        m_WhiteSpaceRegEx = new Regex("\\s", RegexOptions.CultureInvariant);
        m_TrimWhiteSpace = true;
        m_Position = 0;
        m_PeekPosition = 0;
        m_CharsRead = 0;
        m_NeedPropertyCheck = true;
        m_Buffer = new char[4096];
        m_HasFieldsEnclosedInQuotes = true;
        m_MaxLineSize = 10000000;
        m_MaxBufferSize = 10000000;
        m_LeaveOpen = false;
        m_LeaveOpen = leaveOpen;
        InitializeFromStream(stream, defaultEncoding, detectEncoding);
    }

    //
    // Summary:
    //     Initializes a new instance of the TextFieldParser class.
    //
    // Parameters:
    //   reader:
    //     System.IO.TextReader. The System.IO.TextReader stream to be parsed.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     reader is Nothing.
    [HostProtection(SecurityAction.LinkDemand, Resources = HostProtectionResource.ExternalProcessMgmt)]
    public TextFieldParser(TextReader reader)
    {
        m_CommentTokens = new string[0];
        m_LineNumber = 1L;
        m_EndOfData = false;
        m_ErrorLine = "";
        m_ErrorLineNumber = -1L;
        m_TextFieldType = FieldType.Delimited;
        m_WhitespaceCodes = new int[23]
        {
            9, 11, 12, 32, 133, 160, 5760, 8192, 8193, 8194,
            8195, 8196, 8197, 8198, 8199, 8200, 8201, 8202, 8203, 8232,
            8233, 12288, 65279
        };
        m_WhiteSpaceRegEx = new Regex("\\s", RegexOptions.CultureInvariant);
        m_TrimWhiteSpace = true;
        m_Position = 0;
        m_PeekPosition = 0;
        m_CharsRead = 0;
        m_NeedPropertyCheck = true;
        m_Buffer = new char[4096];
        m_HasFieldsEnclosedInQuotes = true;
        m_MaxLineSize = 10000000;
        m_MaxBufferSize = 10000000;
        m_LeaveOpen = false;
        if (reader == null)
        {
            throw ExceptionUtils.GetArgumentNullException("reader");
        }

        m_Reader = reader;
        ReadToBuffer();
    }

    //
    // Summary:
    //     Sets the delimiters for the reader to the specified values, and sets the field
    //     type to Delimited.
    //
    // Parameters:
    //   delimiters:
    //     Array of type String.
    //
    // Exceptions:
    //   T:System.ArgumentException:
    //     A delimiter is zero-length.
    public void SetDelimiters(params string[] delimiters)
    {
        Delimiters = delimiters;
    }

    //
    // Summary:
    //     Sets the delimiters for the reader to the specified values.
    //
    // Parameters:
    //   fieldWidths:
    //     Array of Integer.
    public void SetFieldWidths(params int[] fieldWidths)
    {
        FieldWidths = fieldWidths;
    }

    //
    // Summary:
    //     Returns the current line as a string and advances the cursor to the next line.
    //
    //
    // Returns:
    //     The current line from the file or stream.
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public string ReadLine()
    {
        if ((m_Reader == null) | (m_Buffer == null))
        {
            return null;
        }

        ChangeBufferFunction changeBuffer = ReadToBuffer;
        string text = ReadNextLine(ref m_Position, changeBuffer);
        if (text == null)
        {
            FinishReading();
            return null;
        }

        checked
        {
            m_LineNumber++;
            return text.TrimEnd('\r', '\n');
        }
    }

    //
    // Summary:
    //     Reads all fields on the current line, returns them as an array of strings, and
    //     advances the cursor to the next line containing data.
    //
    // Returns:
    //     An array of strings that contains field values for the current line.
    //
    // Exceptions:
    //   T:Microsoft.VisualBasic.FileIO.MalformedLineException:
    //     A field cannot be parsed by using the specified format.
    public string[] ReadFields()
    {
        if ((m_Reader == null) | (m_Buffer == null))
        {
            return null;
        }

        ValidateReadyToRead();
        return m_TextFieldType switch
        {
            FieldType.FixedWidth => ParseFixedWidthLine(),
            FieldType.Delimited => ParseDelimitedLine(),
            _ => null,
        };
    }

    //
    // Summary:
    //     Reads the specified number of characters without advancing the cursor.
    //
    // Parameters:
    //   numberOfChars:
    //     Int32. Number of characters to read. Required.
    //
    // Returns:
    //     A string that contains the specified number of characters read.
    //
    // Exceptions:
    //   T:System.ArgumentException:
    //     numberOfChars is less than 0.
    public string PeekChars(int numberOfChars)
    {
        if (numberOfChars <= 0)
        {
            throw ExceptionUtils.GetArgumentExceptionWithArgName("numberOfChars", "TextFieldParser_NumberOfCharsMustBePositive", "numberOfChars");
        }

        if ((m_Reader == null) | (m_Buffer == null))
        {
            return null;
        }

        if (m_EndOfData)
        {
            return null;
        }

        string text = PeekNextDataLine();
        if (text == null)
        {
            m_EndOfData = true;
            return null;
        }

        text = text.TrimEnd('\r', '\n');
        if (text.Length < numberOfChars)
        {
            return text;
        }

        return new StringInfo(text).SubstringByTextElements(0, numberOfChars);
    }

    //
    // Summary:
    //     Reads the remainder of the text file and returns it as a string.
    //
    // Returns:
    //     The remaining text from the file or stream.
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public string ReadToEnd()
    {
        if ((m_Reader == null) | (m_Buffer == null))
        {
            return null;
        }

        StringBuilder stringBuilder = new StringBuilder(m_Buffer.Length);
        stringBuilder.Append(m_Buffer, m_Position, checked(m_CharsRead - m_Position));
        stringBuilder.Append(m_Reader.ReadToEnd());
        FinishReading();
        return stringBuilder.ToString();
    }

    //
    // Summary:
    //     Closes the current TextFieldParser object.
    public void Close()
    {
        CloseReader();
    }

    //
    // Summary:
    //     Releases resources used by the Microsoft.VisualBasic.FileIO.TextFieldParser object.
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    void IDisposable.Dispose()
    {
        //ILSpy generated this explicit interface implementation from .override directive in Dispose
        this.Dispose();
    }

    //
    // Summary:
    //     Releases resources used by the Microsoft.VisualBasic.FileIO.TextFieldParser object.
    //
    //
    // Parameters:
    //   disposing:
    //     Boolean. True releases both managed and unmanaged resources; False releases only
    //     unmanaged resources.
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (!m_Disposed)
            {
                Close();
            }

            m_Disposed = true;
        }
    }

    private void ValidateFieldTypeEnumValue(FieldType value, string paramName)
    {
        if (value < FieldType.Delimited || value > FieldType.FixedWidth)
        {
            throw new InvalidEnumArgumentException(paramName, (int)value, typeof(FieldType));
        }
    }

    //
    // Summary:
    //     Allows the Microsoft.VisualBasic.FileIO.TextFieldParser object to attempt to
    //     free resources and perform other cleanup operations before it is reclaimed by
    //     garbage collection.
    ~TextFieldParser()
    {
        Dispose(disposing: false);
        base.Finalize();
    }

    private void CloseReader()
    {
        FinishReading();
        if (m_Reader != null)
        {
            if (!m_LeaveOpen)
            {
                m_Reader.Close();
            }

            m_Reader = null;
        }
    }

    private void FinishReading()
    {
        m_LineNumber = -1L;
        m_EndOfData = true;
        m_Buffer = null;
        m_DelimiterRegex = null;
        m_BeginQuotesRegex = null;
    }

    private void InitializeFromPath(string path, Encoding defaultEncoding, bool detectEncoding)
    {
        if (Operators.CompareString(path, "", TextCompare: false) == 0)
        {
            throw ExceptionUtils.GetArgumentNullException("path");
        }

        if (defaultEncoding == null)
        {
            throw ExceptionUtils.GetArgumentNullException("defaultEncoding");
        }

        string path2 = ValidatePath(path);
        FileStream stream = new FileStream(path2, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        m_Reader = new StreamReader(stream, defaultEncoding, detectEncoding);
        ReadToBuffer();
    }

    private void InitializeFromStream(Stream stream, Encoding defaultEncoding, bool detectEncoding)
    {
        if (stream == null)
        {
            throw ExceptionUtils.GetArgumentNullException("stream");
        }

        if (!stream.CanRead)
        {
            throw ExceptionUtils.GetArgumentExceptionWithArgName("stream", "TextFieldParser_StreamNotReadable", "stream");
        }

        if (defaultEncoding == null)
        {
            throw ExceptionUtils.GetArgumentNullException("defaultEncoding");
        }

        m_Reader = new StreamReader(stream, defaultEncoding, detectEncoding);
        ReadToBuffer();
    }

    private string ValidatePath(string path)
    {
        string text = FileSystem.NormalizeFilePath(path, "path");
        if (!File.Exists(text))
        {
            throw new FileNotFoundException(Utils.GetResourceString("IO_FileNotFound_Path", text));
        }

        return text;
    }

    private bool IgnoreLine(string line)
    {
        if (line == null)
        {
            return false;
        }

        string text = line.Trim();
        if (text.Length == 0)
        {
            return true;
        }

        if (m_CommentTokens != null)
        {
            string[] commentTokens = m_CommentTokens;
            foreach (string text2 in commentTokens)
            {
                if (Operators.CompareString(text2, "", TextCompare: false) != 0)
                {
                    if (text.StartsWith(text2, StringComparison.Ordinal))
                    {
                        return true;
                    }

                    if (line.StartsWith(text2, StringComparison.Ordinal))
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    private int ReadToBuffer()
    {
        m_Position = 0;
        int num = m_Buffer.Length;
        if (num > 4096)
        {
            num = 4096;
            m_Buffer = new char[checked(num - 1 + 1)];
        }

        m_CharsRead = m_Reader.Read(m_Buffer, 0, num);
        return m_CharsRead;
    }

    private int SlideCursorToStartOfBuffer()
    {
        checked
        {
            if (m_Position > 0)
            {
                int num = m_Buffer.Length;
                int num2 = m_CharsRead - m_Position;
                char[] array = new char[num - 1 + 1];
                Array.Copy(m_Buffer, m_Position, array, 0, num2);
                int num3 = m_Reader.Read(array, num2, num - num2);
                m_CharsRead = num2 + num3;
                m_Position = 0;
                m_Buffer = array;
                return num3;
            }

            return 0;
        }
    }

    private int IncreaseBufferSize()
    {
        m_PeekPosition = m_CharsRead;
        checked
        {
            int num = m_Buffer.Length + 4096;
            if (num > m_MaxBufferSize)
            {
                throw ExceptionUtils.GetInvalidOperationException("TextFieldParser_BufferExceededMaxSize");
            }

            char[] array = new char[num - 1 + 1];
            Array.Copy(m_Buffer, array, m_Buffer.Length);
            int num2 = m_Reader.Read(array, m_Buffer.Length, 4096);
            m_Buffer = array;
            m_CharsRead += num2;
            return num2;
        }
    }

    private string ReadNextDataLine()
    {
        ChangeBufferFunction changeBuffer = ReadToBuffer;
        checked
        {
            string text;
            do
            {
                text = ReadNextLine(ref m_Position, changeBuffer);
                m_LineNumber++;
            }
            while (IgnoreLine(text));
            if (text == null)
            {
                CloseReader();
            }

            return text;
        }
    }

    private string PeekNextDataLine()
    {
        ChangeBufferFunction changeBuffer = IncreaseBufferSize;
        SlideCursorToStartOfBuffer();
        m_PeekPosition = 0;
        string text;
        do
        {
            text = ReadNextLine(ref m_PeekPosition, changeBuffer);
        }
        while (IgnoreLine(text));
        return text;
    }

    private string ReadNextLine(ref int Cursor, ChangeBufferFunction ChangeBuffer)
    {
        if (Cursor == m_CharsRead && ChangeBuffer() == 0)
        {
            return null;
        }

        StringBuilder stringBuilder = null;
        checked
        {
            do
            {
                int num = Cursor;
                int num2 = m_CharsRead - 1;
                for (int i = num; i <= num2; i++)
                {
                    char c = m_Buffer[i];
                    if (!((Operators.CompareString(Conversions.ToString(c), "\r", TextCompare: false) == 0) | (Operators.CompareString(Conversions.ToString(c), "\n", TextCompare: false) == 0)))
                    {
                        continue;
                    }

                    if (stringBuilder != null)
                    {
                        stringBuilder.Append(m_Buffer, Cursor, i - Cursor + 1);
                    }
                    else
                    {
                        stringBuilder = new StringBuilder(i + 1);
                        stringBuilder.Append(m_Buffer, Cursor, i - Cursor + 1);
                    }

                    Cursor = i + 1;
                    if (Operators.CompareString(Conversions.ToString(c), "\r", TextCompare: false) == 0)
                    {
                        if (Cursor < m_CharsRead)
                        {
                            if (Operators.CompareString(Conversions.ToString(m_Buffer[Cursor]), "\n", TextCompare: false) == 0)
                            {
                                Cursor++;
                                stringBuilder.Append("\n");
                            }
                        }
                        else if (ChangeBuffer() > 0 && Operators.CompareString(Conversions.ToString(m_Buffer[Cursor]), "\n", TextCompare: false) == 0)
                        {
                            Cursor++;
                            stringBuilder.Append("\n");
                        }
                    }

                    return stringBuilder.ToString();
                }

                int num3 = m_CharsRead - Cursor;
                if (stringBuilder == null)
                {
                    stringBuilder = new StringBuilder(num3 + 10);
                }

                stringBuilder.Append(m_Buffer, Cursor, num3);
            }
            while (ChangeBuffer() > 0);
            return stringBuilder.ToString();
        }
    }

    private string[] ParseDelimitedLine()
    {
        string text = ReadNextDataLine();
        if (text == null)
        {
            return null;
        }

        checked
        {
            long num = m_LineNumber - 1;
            int num2 = 0;
            List<string> list = new List<string>();
            int endOfLineIndex = GetEndOfLineIndex(text);
            while (num2 <= endOfLineIndex)
            {
                Match match = null;
                bool flag = false;
                if (m_HasFieldsEnclosedInQuotes)
                {
                    match = BeginQuotesRegex.Match(text, num2);
                    flag = match.Success;
                }

                string text2;
                if (flag)
                {
                    num2 = match.Index + match.Length;
                    QuoteDelimitedFieldBuilder quoteDelimitedFieldBuilder = new QuoteDelimitedFieldBuilder(m_DelimiterWithEndCharsRegex, m_SpaceChars);
                    quoteDelimitedFieldBuilder.BuildField(text, num2);
                    if (quoteDelimitedFieldBuilder.MalformedLine)
                    {
                        m_ErrorLine = text.TrimEnd('\r', '\n');
                        m_ErrorLineNumber = num;
                        throw new MalformedLineException(Utils.GetResourceString("TextFieldParser_MalFormedDelimitedLine", num.ToString(CultureInfo.InvariantCulture)), num);
                    }

                    if (quoteDelimitedFieldBuilder.FieldFinished)
                    {
                        text2 = quoteDelimitedFieldBuilder.Field;
                        num2 = quoteDelimitedFieldBuilder.Index + quoteDelimitedFieldBuilder.DelimiterLength;
                    }
                    else
                    {
                        do
                        {
                            int length = text.Length;
                            string text3 = ReadNextDataLine();
                            if (text3 == null)
                            {
                                m_ErrorLine = text.TrimEnd('\r', '\n');
                                m_ErrorLineNumber = num;
                                throw new MalformedLineException(Utils.GetResourceString("TextFieldParser_MalFormedDelimitedLine", num.ToString(CultureInfo.InvariantCulture)), num);
                            }

                            if (text.Length + text3.Length > m_MaxLineSize)
                            {
                                m_ErrorLine = text.TrimEnd('\r', '\n');
                                m_ErrorLineNumber = num;
                                throw new MalformedLineException(Utils.GetResourceString("TextFieldParser_MaxLineSizeExceeded", num.ToString(CultureInfo.InvariantCulture)), num);
                            }

                            text += text3;
                            endOfLineIndex = GetEndOfLineIndex(text);
                            quoteDelimitedFieldBuilder.BuildField(text, length);
                            if (quoteDelimitedFieldBuilder.MalformedLine)
                            {
                                m_ErrorLine = text.TrimEnd('\r', '\n');
                                m_ErrorLineNumber = num;
                                throw new MalformedLineException(Utils.GetResourceString("TextFieldParser_MalFormedDelimitedLine", num.ToString(CultureInfo.InvariantCulture)), num);
                            }
                        }
                        while (!quoteDelimitedFieldBuilder.FieldFinished);
                        text2 = quoteDelimitedFieldBuilder.Field;
                        num2 = quoteDelimitedFieldBuilder.Index + quoteDelimitedFieldBuilder.DelimiterLength;
                    }

                    if (m_TrimWhiteSpace)
                    {
                        text2 = text2.Trim();
                    }

                    list.Add(text2);
                    continue;
                }

                Match match2 = m_DelimiterRegex.Match(text, num2);
                if (match2.Success)
                {
                    text2 = text.Substring(num2, match2.Index - num2);
                    if (m_TrimWhiteSpace)
                    {
                        text2 = text2.Trim();
                    }

                    list.Add(text2);
                    num2 = match2.Index + match2.Length;
                    continue;
                }

                text2 = text.Substring(num2).TrimEnd('\r', '\n');
                if (m_TrimWhiteSpace)
                {
                    text2 = text2.Trim();
                }

                list.Add(text2);
                break;
            }

            return list.ToArray();
        }
    }

    private string[] ParseFixedWidthLine()
    {
        string text = ReadNextDataLine();
        if (text == null)
        {
            return null;
        }

        text = text.TrimEnd('\r', '\n');
        StringInfo line = new StringInfo(text);
        checked
        {
            ValidateFixedWidthLine(line, m_LineNumber - 1);
            int num = 0;
            int num2 = m_FieldWidths.Length - 1;
            string[] array = new string[num2 + 1];
            int num3 = num2;
            for (int i = 0; i <= num3; i++)
            {
                array[i] = GetFixedWidthField(line, num, m_FieldWidths[i]);
                num += m_FieldWidths[i];
            }

            return array;
        }
    }

    private string GetFixedWidthField(StringInfo Line, int Index, int FieldLength)
    {
        string text = ((FieldLength > 0) ? Line.SubstringByTextElements(Index, FieldLength) : ((Index < Line.LengthInTextElements) ? Line.SubstringByTextElements(Index).TrimEnd('\r', '\n') : string.Empty));
        if (m_TrimWhiteSpace)
        {
            return text.Trim();
        }

        return text;
    }

    private int GetEndOfLineIndex(string Line)
    {
        int length = Line.Length;
        if (length == 1)
        {
            return length;
        }

        checked
        {
            if ((Operators.CompareString(Conversions.ToString(Line[length - 2]), "\r", TextCompare: false) == 0) | (Operators.CompareString(Conversions.ToString(Line[length - 2]), "\n", TextCompare: false) == 0))
            {
                return length - 2;
            }

            if ((Operators.CompareString(Conversions.ToString(Line[length - 1]), "\r", TextCompare: false) == 0) | (Operators.CompareString(Conversions.ToString(Line[length - 1]), "\n", TextCompare: false) == 0))
            {
                return length - 1;
            }

            return length;
        }
    }

    private void ValidateFixedWidthLine(StringInfo Line, long LineNumber)
    {
        if (Line.LengthInTextElements < m_LineLength)
        {
            m_ErrorLine = Line.String;
            m_ErrorLineNumber = checked(m_LineNumber - 1);
            throw new MalformedLineException(Utils.GetResourceString("TextFieldParser_MalFormedFixedWidthLine", LineNumber.ToString(CultureInfo.InvariantCulture)), LineNumber);
        }
    }

    private void ValidateFieldWidths()
    {
        if (m_FieldWidths == null)
        {
            throw ExceptionUtils.GetInvalidOperationException("TextFieldParser_FieldWidthsNothing");
        }

        if (m_FieldWidths.Length == 0)
        {
            throw ExceptionUtils.GetInvalidOperationException("TextFieldParser_FieldWidthsNothing");
        }

        checked
        {
            int num = m_FieldWidths.Length - 1;
            m_LineLength = 0;
            int num2 = num - 1;
            for (int i = 0; i <= num2; i++)
            {
                m_LineLength += m_FieldWidths[i];
            }

            if (m_FieldWidths[num] > 0)
            {
                m_LineLength += m_FieldWidths[num];
            }
        }
    }

    private void ValidateFieldWidthsOnInput(int[] Widths)
    {
        checked
        {
            int num = Widths.Length - 1 - 1;
            for (int i = 0; i <= num; i++)
            {
                if (Widths[i] < 1)
                {
                    throw ExceptionUtils.GetArgumentExceptionWithArgName("FieldWidths", "TextFieldParser_FieldWidthsMustPositive", "FieldWidths");
                }
            }
        }
    }

    private void ValidateAndEscapeDelimiters()
    {
        if (m_Delimiters == null)
        {
            throw ExceptionUtils.GetArgumentExceptionWithArgName("Delimiters", "TextFieldParser_DelimitersNothing", "Delimiters");
        }

        if (m_Delimiters.Length == 0)
        {
            throw ExceptionUtils.GetArgumentExceptionWithArgName("Delimiters", "TextFieldParser_DelimitersNothing", "Delimiters");
        }

        int num = m_Delimiters.Length;
        StringBuilder stringBuilder = new StringBuilder();
        StringBuilder stringBuilder2 = new StringBuilder();
        stringBuilder2.Append(EndQuotePattern + "(");
        checked
        {
            int num2 = num - 1;
            for (int i = 0; i <= num2; i++)
            {
                if (m_Delimiters[i] != null)
                {
                    if (m_HasFieldsEnclosedInQuotes && m_Delimiters[i].IndexOf('"') > -1)
                    {
                        throw ExceptionUtils.GetInvalidOperationException("TextFieldParser_IllegalDelimiter");
                    }

                    string text = Regex.Escape(m_Delimiters[i]);
                    stringBuilder.Append(text + "|");
                    stringBuilder2.Append(text + "|");
                }
            }

            m_SpaceChars = WhitespaceCharacters;
            m_DelimiterRegex = new Regex(stringBuilder.ToString(0, stringBuilder.Length - 1), RegexOptions.CultureInvariant);
            stringBuilder.Append("\r|\n");
            m_DelimiterWithEndCharsRegex = new Regex(stringBuilder.ToString(), RegexOptions.CultureInvariant);
            stringBuilder2.Append("\r|\n)|\"$");
        }
    }

    private void ValidateReadyToRead()
    {
        if (!(m_NeedPropertyCheck | ArrayHasChanged()))
        {
            return;
        }

        switch (m_TextFieldType)
        {
            case FieldType.Delimited:
                ValidateAndEscapeDelimiters();
                break;
            case FieldType.FixedWidth:
                ValidateFieldWidths();
                break;
        }

        if (m_CommentTokens != null)
        {
            string[] commentTokens = m_CommentTokens;
            foreach (string text in commentTokens)
            {
                if (Operators.CompareString(text, "", TextCompare: false) != 0 && (m_HasFieldsEnclosedInQuotes & (m_TextFieldType == FieldType.Delimited)) && string.Compare(text.Trim(), "\"", StringComparison.Ordinal) == 0)
                {
                    throw ExceptionUtils.GetInvalidOperationException("TextFieldParser_InvalidComment");
                }
            }
        }

        m_NeedPropertyCheck = false;
    }

    private void ValidateDelimiters(string[] delimiterArray)
    {
        if (delimiterArray == null)
        {
            return;
        }

        foreach (string text in delimiterArray)
        {
            if (Operators.CompareString(text, "", TextCompare: false) == 0)
            {
                throw ExceptionUtils.GetArgumentExceptionWithArgName("Delimiters", "TextFieldParser_DelimiterNothing", "Delimiters");
            }

            if (text.IndexOfAny(new char[2] { '\r', '\n' }) > -1)
            {
                throw ExceptionUtils.GetArgumentExceptionWithArgName("Delimiters", "TextFieldParser_EndCharsInDelimiter");
            }
        }
    }

    private bool ArrayHasChanged()
    {
        int num = 0;
        checked
        {
            switch (m_TextFieldType)
            {
                case FieldType.Delimited:
                    {
                        if (m_Delimiters == null)
                        {
                            return false;
                        }

                        num = m_DelimitersCopy.GetLowerBound(0);
                        int upperBound2 = m_DelimitersCopy.GetUpperBound(0);
                        int num4 = num;
                        int num5 = upperBound2;
                        for (int j = num4; j <= num5; j++)
                        {
                            if (Operators.CompareString(m_Delimiters[j], m_DelimitersCopy[j], TextCompare: false) != 0)
                            {
                                return true;
                            }
                        }

                        break;
                    }
                case FieldType.FixedWidth:
                    {
                        if (m_FieldWidths == null)
                        {
                            return false;
                        }

                        num = m_FieldWidthsCopy.GetLowerBound(0);
                        int upperBound = m_FieldWidthsCopy.GetUpperBound(0);
                        int num2 = num;
                        int num3 = upperBound;
                        for (int i = num2; i <= num3; i++)
                        {
                            if (m_FieldWidths[i] != m_FieldWidthsCopy[i])
                            {
                                return true;
                            }
                        }

                        break;
                    }
            }

            return false;
        }
    }

    private void CheckCommentTokensForWhitespace(string[] tokens)
    {
        if (tokens == null)
        {
            return;
        }

        foreach (string input in tokens)
        {
            if (m_WhiteSpaceRegEx.IsMatch(input))
            {
                throw ExceptionUtils.GetArgumentExceptionWithArgName("CommentTokens", "TextFieldParser_WhitespaceInToken");
            }
        }
    }

    private bool CharacterIsInDelimiter(char testCharacter)
    {
        string[] delimiters = m_Delimiters;
        for (int i = 0; i < delimiters.Length; i = checked(i + 1))
        {
            if (delimiters[i].IndexOf(testCharacter) > -1)
            {
                return true;
            }
        }

        return false;
    }
}
#if false // Decompilation log
'66' items in cache
------------------
Resolve: 'mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Found single assembly: 'mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Load from: 'C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\mscorlib.dll'
------------------
Resolve: 'System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Found single assembly: 'System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Load from: 'C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.dll'
------------------
Resolve: 'System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Found single assembly: 'System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Load from: 'C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Windows.Forms.dll'
------------------
Resolve: 'System.Deployment, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Deployment, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Deployment.dll'
------------------
Resolve: 'System.Management, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Could not find by name: 'System.Management, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
------------------
Resolve: 'System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Found single assembly: 'System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Load from: 'C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Core.dll'
------------------
Resolve: 'System.Xml.Linq, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Found single assembly: 'System.Xml.Linq, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Load from: 'C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Xml.Linq.dll'
------------------
Resolve: 'System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.dll'
------------------
Resolve: 'System.Runtime.Remoting, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Could not find by name: 'System.Runtime.Remoting, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
#endif

