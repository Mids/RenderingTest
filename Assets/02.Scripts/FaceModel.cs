using System.Collections;
using UnityEngine;

public class FaceModel : MonoBehaviour
{
	private int[] _faceList;

	private FileParser _fileParser;

	private Mesh _mesh;

	private MeshFilter _meshFilter;
	private Vector3[] _vertexList;

	// Use this for initialization
	private void Start()
	{
		_fileParser = GetComponent<FileParser>();
		_meshFilter = GetComponent<MeshFilter>();

		StartCoroutine(LoadFace());
	}

	private IEnumerator LoadFace()
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
	private void Update()
	{
		var temp = _fileParser.GetVertices();
		if (temp != null)
			_mesh.vertices = temp;
	}
}