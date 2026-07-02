using Kinetra.Graphics;

namespace Kinetra.Core
{
public class ObjLoader
{
    public Mesh Load(string path)
    {
        var positions = new List<Vector3>();
        var texcoords = new List<Vector2>();
        var normals = new List<Vector3>();

        var vertices = new List<float>();
        var indices = new List<uint>();
        var vertexMap = new Dictionary<string, uint>();

        uint index = 0;

        foreach (var line in File.ReadLines(path))
        {
            var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 0) continue;

            switch (parts[0])
            {
                case "v":
                    positions.Add(new Vector3(
                        float.Parse(parts[1]),
                        float.Parse(parts[2]),
                        float.Parse(parts[3])
                    ));
                    break;

                case "vt":
                    texcoords.Add(new Vector2(
                        float.Parse(parts[1]),
                        float.Parse(parts[2])
                    ));
                    break;

                case "vn":
                    normals.Add(new Vector3(
                        float.Parse(parts[1]),
                        float.Parse(parts[2]),
                        float.Parse(parts[3])
                    ));
                    break;

                case "f":
                    for (int i = 1; i <= 3; i++) // triangulado
                    {
                        var key = parts[i];

                        if (!vertexMap.TryGetValue(key, out uint vertIndex))
                        {
                            var indicesSplit = key.Split('/');

                            int vi = int.Parse(indicesSplit[0]) - 1;

                            int ti = indicesSplit.Length > 1 && indicesSplit[1] != ""
                                ? int.Parse(indicesSplit[1]) - 1
                                : -1;

                            int ni = indicesSplit.Length > 2 && indicesSplit[2] != ""
                                ? int.Parse(indicesSplit[2]) - 1
                                : -1;

                            var pos = positions[vi];
                            var uv = ti >= 0 ? texcoords[ti] : Vector2.Zero;
                            var norm = ni >= 0 ? normals[ni] : Vector3.UnitY;

                            vertices.Add(pos.X);
                            vertices.Add(pos.Y);
                            vertices.Add(pos.Z);

                            vertices.Add(uv.X);
                            vertices.Add(uv.Y);

                            vertices.Add(norm.X);
                            vertices.Add(norm.Y);
                            vertices.Add(norm.Z);

                            vertexMap[key] = index;
                            vertIndex = index;
                            index++;
                        }

                        indices.Add(vertIndex);
                    }
                    break;
            }
        }

        var mesh = new Mesh(vertices.ToArray(), indices.ToArray());

        mesh.layout.AddAttribute(0, 3); // pos
        mesh.layout.AddAttribute(1, 2); // uv
        mesh.layout.AddAttribute(2, 3); // normal

        return mesh;
    }
}
}