namespace PruebaTecnicaVecctor.Genericos
{
    public enum ResultType
    {
        Ok, NotFound, BadArguments
    }
    public class ServiceResult<T>
    {
        public ServiceResult()
        {
            Type = ResultType.Ok;
            Errors = new List<string>();
            Messages = new List<string>();
        }

        public T Data { get; set; }
        public ResultType Type { get; set; }
        public List<string> Errors { get; set; }
        public List<string> Messages { get; set; }
    }
}
