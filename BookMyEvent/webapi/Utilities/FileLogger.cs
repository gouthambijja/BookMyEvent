using System.IO;

namespace BookMyEvent.WebApi.Utilities
{
    public class FileLogger
    {
        public  void AddInfoToFile(string textToAdd)
        {
            try
            {
                // Append text to the file
                string filePath = "logs/logs.txt";
                string text = DateTime.Now + ": " + "[INFO] " + textToAdd;
                File.AppendAllText(filePath, text + "\n");
            }
            catch (IOException ex)
            {
                throw new FileOperationException($"An error occurred while adding text to the file: {ex.Message}");
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new FileOperationException($"Access to the file is unauthorized: {ex.Message}");
            }
            //catch (PathTooLongException ex)
            //{
            //    throw new FileOperationException($"The file path is too long: {ex.Message}");
            //}
            catch (Exception ex)
            {
                throw new FileOperationException($"An error occurred: {ex.Message}");
            }
        }

        public void AddExceptionToFile(string textToAdd)
        {
            try
            {
                // Append text to the file
                string filePath = "logs/logs.txt";
                string text = DateTime.Now + ": " + "[EXC] " + textToAdd;
                File.AppendAllText(filePath,text + "\n");
            }
            catch (IOException ex)
            {
                throw new FileOperationException($"An error occurred while adding text to the file: {ex.Message}");
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new FileOperationException($"Access to the file is unauthorized: {ex.Message}");
            }
            //catch (PathTooLongException ex)
            //{
            //    throw new FileOperationException($"The file path is too long: {ex.Message}");
            //}
            catch (Exception ex)
            {
                throw new FileOperationException($"An error occurred: {ex.Message}");
            }
        }
    }
        public class FileOperationException : Exception
        {
            public FileOperationException(string message) : base(message)
            {
            }
        }
    

}
