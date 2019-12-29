using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileParser : MonoBehaviour
{
	public TextAsset VertexFile;
	public TextAsset FaceFile;

	private StreamReader _vertexReader;
	public int vectexCount;

	private void Start()
	{
		_vertexReader = new StreamReader(new MemoryStream(VertexFile.bytes));
		vectexCount = Convert.ToInt32(_vertexReader.ReadLine());

		print($"vertex count : {vectexCount}");
	}


	public Vector3[] GetVertices()
	{
		var vertices = new Vector3[vectexCount];

		for (int i = 0; i < vectexCount; i++)
		{
			string line = _vertexReader.ReadLine();
			string[] bits = line?.Split('\t');
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
		StreamReader faceReader = new StreamReader(new MemoryStream(FaceFile.bytes));
		int count = Convert.ToInt32(faceReader.ReadLine());

		print($"face count : {count}");

		var facet = new int[count * 3];

		for (int i = 0; i < count; i++)
		{
			string line = faceReader.ReadLine();
			string[] bits = line?.Split('\t');
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

			for (int j = 0; j < 3; j++)
			{
				facet[i * 3 + j] = Convert.ToInt32(bits[j]);
			}
		}

		faceReader.Close();

		return facet;
	}

	private void OnDestroy()
	{
		_vertexReader.Close();
	}
}