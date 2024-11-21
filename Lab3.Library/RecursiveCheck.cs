using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Lab3.Library
{
    public class RecursiveCheck
    {
        // Клас для збереження інформації про процедури
        public class Procedure
        {
            public string Id { get; }
            public List<string> CalledProcedures { get; }

            public Procedure(string id, List<string> calledProcedures)
            {
                Id = id;
                CalledProcedures = calledProcedures;
            }
        }

        public static (List<Procedure> procedures, int n) ReadInput(string[] lines)
        {
            List<Procedure> procedures = new List<Procedure>();
            int lineIndex = 0;

            if (!int.TryParse(lines[lineIndex], out int n) || n < 1 || n > 100)
            {
                throw new InvalidOperationException("The first line must contain an integer n (1 <= n <= 100)!");
            }
            lineIndex++;

            for (int i = 0; i < n; i++)
            {
                string procedureId = lines[lineIndex].Trim();
                if (string.IsNullOrWhiteSpace(procedureId) || procedureId.Length > 100 || !IsValidIdentifier(procedureId))
                {
                    throw new InvalidOperationException($"Invalid procedure identifier at line {lineIndex + 1}. It must be non-empty, less than 100 characters, and contain only lowercase letters or digits.");
                }
                lineIndex++;

                if (!int.TryParse(lines[lineIndex], out int k) || k < 0 || k > n)
                {
                    throw new InvalidOperationException($"Invalid number of called procedures for procedure {procedureId} at line {lineIndex + 1}. It must be an integer between 0 and n.");
                }
                lineIndex++;

                List<string> calledProcedures = new List<string>();
                for (int j = 0; j < k; j++)
                {
                    string calledProcedureId = lines[lineIndex].Trim();
                    if (string.IsNullOrWhiteSpace(calledProcedureId) || !IsValidIdentifier(calledProcedureId))
                    {
                        throw new InvalidOperationException($"Invalid called procedure identifier at line {lineIndex + 1}. It must be non-empty and contain only lowercase letters or digits.");
                    }
                    calledProcedures.Add(calledProcedureId);
                    lineIndex++;
                }

                procedures.Add(new Procedure(procedureId, calledProcedures));

                if (lines[lineIndex] != "*****")
                {
                    throw new InvalidOperationException($"Missing termination line (*****) for procedure {procedureId} at line {lineIndex + 1}.");
                }
                lineIndex++;
            }

            return (procedures, n);
        }

        public static bool IsValidIdentifier(string identifier)
        {
            if (string.IsNullOrWhiteSpace(identifier) || identifier.Length == 0)
            {
                return false;
            }
            foreach (char c in identifier)
            {
                if (!char.IsLower(c) && !char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }

        public static StringBuilder CheckProcedures(List<Procedure> procedures)
        {
            Dictionary<string, Procedure> procedureDict = procedures.ToDictionary(p => p.Id, p => p);
            Dictionary<string, bool> isRecursive = procedures.ToDictionary(p => p.Id, p => false); // тримаємо результат для кожної процедури
            StringBuilder result = new StringBuilder();

            foreach (var procedure in procedures)
            {
                HashSet<string> visited = new HashSet<string>();
                if (!isRecursive[procedure.Id])
                {
                    DFS(procedure.Id, procedure.Id, visited, procedureDict, isRecursive);
                }

                result.AppendLine(isRecursive[procedure.Id] ? "YES" : "NO");
            }

            return result;
        }

        public static bool DFS(string startProcedure, string currentProcedure, HashSet<string> visited, Dictionary<string, Procedure> procedureDict, Dictionary<string, bool> isRecursive)
        {
            if (visited.Contains(currentProcedure))
            {
                if (currentProcedure == startProcedure)
                {
                    isRecursive[startProcedure] = true;
                    return true;
                }
                return false;
            }

            visited.Add(currentProcedure);

            foreach (var calledProcedureId in procedureDict[currentProcedure].CalledProcedures)
            {
                if (DFS(startProcedure, calledProcedureId, visited, procedureDict, isRecursive))
                {
                    return true;
                }
            }

            visited.Remove(currentProcedure);
            return false;
        }
    }
}