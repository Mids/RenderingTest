using System;
using System.IO;
using UnityEngine;

public class FileParser : MonoBehaviour
{
	private StreamReader _vertexReader;
	public TextAsset FaceFile;
	public int vectexCount;
	public TextAsset VertexFile;

	private void Start()
	{
		_vertexReader = new StreamReader(new MemoryStream(VertexFile.bytes));
		vectexCount = Convert.ToInt32(_vertexReader.ReadLine());

		print($"vertex count : {vectexCount}");
	}


	public Vector3[] GetVertices()
	{
		if (_vertexReader.EndOfStream) return null;

		var vertices = new Vector3[vectexCount];

		for (var i = 0; i < vectexCount; i++)
		{
			var line = _vertexReader.ReadLine();
			var bits = line?.Split('\t');
			if (bits == null)
			{
				print("bits is null in faceData.");
				break;
			}

			if (bits.Length != 3)
			{
				print($"There are {bits.Length} bits in one line.");
				break;
			}

			vertices[i] = new Vector3(Convert.ToSingle(bits[0]), Convert.ToSingle(bits[1]), Convert.ToSingle(bits[2]));
		}


		return vertices;
	}

	public int[] GetFacet()
	{
		var faceReader = new StreamReader(new MemoryStream(FaceFile.bytes));
		var count = Convert.ToInt32(faceReader.ReadLine());

		print($"face count : {count}");

		var facet = new int[count * 3];

		for (var i = 0; i < count; i++)
		{
			var line = faceReader.ReadLine();
			var bits = line?.Split('\t');
			if (bits == null)
			{
				print("bits is null in faceData.");
				break;
			}

			if (bits.Length != 3)
			{
				print($"There are {bits.Length} bits in one line.");
				break;
			}

			for (var j = 0; j < 3; j++) facet[i * 3 + j] = Convert.ToInt32(bits[j]);
		}

		faceReader.Close();

		return facet;
	}

	private void OnDestroy()
	{
		_vertexReader.Close();
	}
}