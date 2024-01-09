using System.Text.Json;

namespace UserCrud 
{
    public class User
    {
        private string _filePath = @"C:\Users\akraz\Desktop\.NET-\MyApp";

        public void CreateFile<T>()
        {
            string myfileName = typeof(T).Name + ".json";
            string Path = System.IO.Path.Combine(_filePath, myfileName);
            FullPath = Path;

            try
            {
                if (!File.Exists(Path))
                {
                    using StreamWriter writer = new StreamWriter(Path);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating file: {ex.Message}");
            }
        }
        private string FullPath;

        public void Add<T>(T obj)
        {
            CreateFile<T>();
            List<T> objects = GetAll<T>();

            for (int i = 0; i < objects.Count; i++)
            {
                if (GetId(objects[i]) == GetId(obj))
                {
                    Console.WriteLine("Already exist");
                    return;
                }
            }

            objects.Add(obj);
            Console.WriteLine("Seccesfully added");

            UpdateDB<T>(Serialize(objects));
        }

        public void Delete<T>(int id)
        {
            List<T> objects = GetAll<T>();
            for (int i = 0; i < objects.Count; i++)
            {
                if (GetId(objects[i]) == id)
                {
                    objects.RemoveAt(i);
                    Console.WriteLine("Seccesfully deleted");
                    UpdateDB<T>(Serialize(objects));
                    return;
                }
            }
            Console.WriteLine("Not found");
        }
        public void Update<T>(int id, T obj)
        {
            List<T> objects = GetAll<T>();

            for (int i = 0; i < objects.Count; i++)
            {
                if (GetId(objects[i]) == id)
                {
                    objects[i] = obj;
                    UpdateDB<T>(Serialize(objects));
                }
            }
        }
        public List<T> GetAll<T>()
        {
            try
            {
                using (StreamReader sr = new StreamReader(FullPath))
                {
                    Console.WriteLine(FullPath);
                    return SDeserialize<T>(sr.ReadToEnd());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file: {ex.Message}");
                return new List<T>();
            }
        }


        public List<T> SDeserialize<T>(string json)
        {
            return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
        }

        public string Serialize<T>(List<T> vals)
        {
            return JsonSerializer.Serialize(vals);
        }

        public void UpdateDB<T>(string json)
        {
            try
            {
                File.WriteAllText(FullPath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to file: {ex.Message}");
            }
        }

        private int GetId<T>(T item)
        {
            string fName = typeof(T).Name;
            var propId = typeof(T).GetProperty("Id");
            var propValue = propId.GetValue(item);
            return (int)propValue;
        }
    }
}

