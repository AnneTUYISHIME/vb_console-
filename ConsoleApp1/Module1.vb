Imports Npgsql

Module Program
    ' Update with your PostgreSQL settings
    Dim connString As String = "Host=localhost;Port=5432;Username=postgres;Password=Anne123;Database=studentdb"

    Public Sub Main()
        Dim choice As Integer

        Do
            Console.WriteLine("Student  Management ")
            Console.WriteLine("1. Add Student")
            Console.WriteLine("2. View Students")
            Console.WriteLine("3. Update Student")
            Console.WriteLine("4. Delete Student")
            Console.WriteLine("5. Exit")
            Console.Write("Enter menu: ")
            Integer.TryParse(Console.ReadLine(), choice)

            Select Case choice
                Case 1 : AddStudent()
                Case 2 : ViewStudents()
                Case 3 : UpdateStudent()
                Case 4 : DeleteStudent()
                Case 5 : Exit Do
                Case Else : Console.WriteLine("Invalid choice.")
            End Select
        Loop
    End Sub

    Sub AddStudent()
        Console.Write("Enter Name: ")
        Dim name = Console.ReadLine()
        Console.Write("Enter Age: ")
        Dim age As Integer
        Integer.TryParse(Console.ReadLine(), age)

        Using conn As New NpgsqlConnection(connString)
            conn.Open()
            Dim cmd As New NpgsqlCommand("INSERT INTO students (name, age) VALUES (@name, @age)", conn)
            cmd.Parameters.AddWithValue("name", name)
            cmd.Parameters.AddWithValue("age", age)
            cmd.ExecuteNonQuery()
            Console.WriteLine("Student added.")
        End Using
    End Sub

    Sub ViewStudents()
        Using conn As New NpgsqlConnection(connString)
            conn.Open()
            Dim cmd As New NpgsqlCommand("SELECT * FROM students", conn)
            Dim reader = cmd.ExecuteReader()

            Console.WriteLine("ID | Name | Age")
            While reader.Read()
                Console.WriteLine($"{reader("id")} | {reader("name")} | {reader("age")}")
            End While
        End Using
    End Sub

    Sub UpdateStudent()
        Console.Write("Enter student ID to update: ")
        Dim id As Integer
        Integer.TryParse(Console.ReadLine(), id)
        Console.Write("Enter new name: ")
        Dim newName = Console.ReadLine()

        ' Ask for new age as well
        Console.Write("Enter new age: ")
        Dim newAge As Integer
        Integer.TryParse(Console.ReadLine(), newAge)

        Using conn As New NpgsqlConnection(connString)
            conn.Open()
            ' Update both name and age
            Dim cmd As New NpgsqlCommand("UPDATE students SET name = @name, age = @age WHERE id = @id", conn)
            cmd.Parameters.AddWithValue("name", newName)
            cmd.Parameters.AddWithValue("age", newAge)
            cmd.Parameters.AddWithValue("id", id)
            cmd.ExecuteNonQuery()
            Console.WriteLine("Student updated.")
        End Using
    End Sub

    Sub DeleteStudent()
        Console.Write("Enter student ID to delete: ")
        Dim id As Integer
        Integer.TryParse(Console.ReadLine(), id)

        Using conn As New NpgsqlConnection(connString)
            conn.Open()
            Dim cmd As New NpgsqlCommand("DELETE FROM students WHERE id = @id", conn)
            cmd.Parameters.AddWithValue("id", id)
            cmd.ExecuteNonQuery()
            Console.WriteLine("Student deleted.")
        End Using
    End Sub
End Module
