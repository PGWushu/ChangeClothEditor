using UnityEngine;
using System.Collections;
using System.Linq;

public class CSVReader /*: MonoBehaviour*/
{
	public TextAsset csvFile;
	public void Start()
	{
		string[,] grid = SplitCsvGrid(csvFile.text);
		Debug.Log("size = " + (1 + grid.GetUpperBound(0)) + "," + (1 + grid.GetUpperBound(1)));

		DebugOutputGrid(grid);
	}
	/// <summary>
	/// 输出2D数组的内容，用于检查导入程序/ outputs the content of a 2D array, useful for checking the importer
	/// </summary>
	/// <param name="grid"></param>
	static public void DebugOutputGrid(string[,] grid)
	{
		string textOutput = "";
		for (int y = 0; y < grid.GetUpperBound(1); y++)
		{
			for (int x = 0; x < grid.GetUpperBound(0); x++)
			{

				textOutput += grid[x, y];
				textOutput += "|";
			}
			textOutput += "\n";
		}
		Debug.Log(textOutput + ">>>>>>输出2D数组的内容，用于检查导入程序");
	}

	/// <summary>
	/// 将CSV文件拆分为二维字符串数组splits a CSV file into a 2D string array
	/// </summary>
	/// <param name="csvText"></param>
	/// <returns></returns>
	static public string[,] SplitCsvGrid(string csvText)
	{
		string[] lines = csvText.Split("\n"[0]);

		//查找行的最大宽度
		// finds the max width of row
		int width = 0;
		for (int i = 0; i < lines.Length; i++)
		{
			string[] row = SplitCsvLine(lines[i]);
			width = Mathf.Max(width, row.Length);
		}
		//创建要输出到的新二维字符串网格
		// creates new 2D string grid to output to
		string[,] outputGrid = new string[width + 1, lines.Length + 1];
		for (int y = 0; y < lines.Length; y++)
		{
			string[] row = SplitCsvLine(lines[y]);
			for (int x = 0; x < row.Length; x++)
			{
				outputGrid[x, y] = row[x];
				// 此行用于将输出中的“”替换为“”。
				// 根据需要包括或编辑它。
				// This line was to replace "" with " in my output. 
				// Include or edit it as you wish.
				outputGrid[x, y] = outputGrid[x, y].Replace("\"\"", "\"");
			}
		}

		return outputGrid;
	}
	//拆分一个CSV行
	// splits a CSV row 
	static public string[] SplitCsvLine(string line)
	{
		return (from System.Text.RegularExpressions.Match m in System.Text.RegularExpressions.Regex.Matches(line,
		@"(((?<x>(?=[,\r\n]+))|""(?<x>([^""]|"""")+)""|(?<x>[^,\r\n]+)),?)",
		System.Text.RegularExpressions.RegexOptions.ExplicitCapture)
				select m.Groups[1].Value).ToArray();
	}
}
