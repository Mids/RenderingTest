using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceModel : MonoBehaviour
{

	private Vector3[] _vertexList;

	private int[] _faceList;

	private Mesh _mesh;

	private FileParser _fileParser;

	private MeshFilter _meshFilter;
	
	// Use this for initialization
	void Start ()
	{
		_fileParser = GetComponent<FileParser>();
		_meshFilter = GetComponent<MeshFilter>();

		StartCoroutine(LoadFace());
	}

	IEnumerator LoadFace()
	{
		_vertexList = _fileParser.GetVertices();
		_faceList = _fileParser.GetFacet();
		_mesh = new Mesh();

		_meshFilter.mesh = _mesh;
		_mesh.name = "Face";
		_mesh.vertices = _vertexList;
		_mesh.triangles = _faceList;
		
		_mesh.RecalculateNormals();
		
		print("Load Face Done");
		yield break;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
