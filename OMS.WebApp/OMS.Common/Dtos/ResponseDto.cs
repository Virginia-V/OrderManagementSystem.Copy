namespace OMS.Common.Dtos
{
    public class ResponseDto<TResult>
    {
        public bool Succeeded { get; set; }
        public string ErrorMessage { get; set; }
        public TResult Result { get; set; }
    }
}
