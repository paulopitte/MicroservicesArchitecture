namespace Core.Common.Models
{
    /// <summary>
    ///  Representa as chaves de configuração.
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// Identificador do endpoit.
        /// </summary>
        public IEnumerable<AppSettingsItem> Settings { get; set; }

        /// <summary>
        /// Obtém uma configuração.
        /// </summary>
        /// <param name="key">Identificador da chave.</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>Configuração se encontrado.</returns>
        public T GetValue<T>(string key)
        {
            var value = Settings?.FirstOrDefault(e => e.Key.Equals(key))?.Value;
            if (string.IsNullOrEmpty(value))
            {
                return default(T);
            }

            return (T)Convert.ChangeType(value, typeof(T));
        }
    }

    /// <summary>
    /// Representa um item de configuração.
    /// </summary>
    public class AppSettingsItem
    {
        /// <summary>
        /// Identificador da configuração.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Valor da chave.
        /// </summary>
        public string Value { get; set; }
    }
}
