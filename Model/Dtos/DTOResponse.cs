namespace Work_Bank_api.Model.Dtos
{
    /// <summary>
    /// resopnse object
    /// </summary>
    /// <typeparam name="T"> the dto or empty json</typeparam>
    public class DTOResponse<T>
    {
        /// <summary>
        /// status sucsess or fail
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// status Code
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// The data object or empty json
        /// </summary>
        public T? Data { get; set; }
    }
}
