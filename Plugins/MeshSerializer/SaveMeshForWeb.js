// Add this to any object. This will save it's mesh
// to a local file for the Web.

var fileName = "SerializedMesh.data";
var saveTangents = false;

function Start()
{
	fileName = gameObject.name + ".3d";

    var inputMesh = GetComponent(MeshFilter).mesh;
    var fullFileName = Application.dataPath + "/" + fileName;
    MeshSerializer.WriteMeshToFileForWeb (inputMesh, fullFileName, saveTangents);
    print ("Saved " + name + " mesh to " + fullFileName );
}
