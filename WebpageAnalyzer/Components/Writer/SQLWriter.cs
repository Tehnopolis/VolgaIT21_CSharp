using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security;

namespace WebpageAnalyzer.Components
{
    /// <summary>
    /// Помогает сохранять результат в SQL базу данных
    /// </summary>
    public sealed class SQLWriter : IWriter
    {
        private string ConnectionString;
        private string DBUser;
        private SecureString DBPassword;

        /// <summary>
        /// Название таблицы в базе данных, в которую будут вставлены данные
        /// </summary>
        public string DatabaseTableName { get; set; } = "Results";
        /// <summary>
        /// Название колонки, где будет указано слово
        /// </summary>
        public string DatabaseWordColumnName { get; set; } = "Word";
        /// <summary>
        /// Название колонки, где будет указано количество (Слова в тексте)
        /// </summary>
        public string DatabaseAmountColumnName { get; set; } = "Amount";

        /// <param name="connectionString">Текст соединения с БД</param>
        /// <param name="user">Пользователь БД</param>
        /// <param name="password">Пороль от пользователя БД</param>
        /// <exception cref="ArgumentNullException">Если не был предоставлен текст для соединения с БД/пользователь/пороль</exception>
        public SQLWriter(string connectionString, string user, SecureString password)
        {
            if (connectionString == null || connectionString == "")
                throw new ArgumentNullException("Текст соединения не был предоставлен");
            else if (user == null || user == "")
                throw new ArgumentNullException("Пользователь БД не был предоставлен");
            else if (password == null || password.Length < 1)
                throw new ArgumentNullException("Пороль БД не был предоставлен");

            this.ConnectionString = connectionString;
            this.DBUser = user;
            this.DBPassword = password;
        }


        /// <summary>
        /// Сохранить в БД
        /// </summary>
        /// <exception cref="InvalidProgramException">Если не удалось выполнить команду</exception>
        public void Write(Dictionary<string, int> data)
        {
            // Открыть БД
            using (SqlConnection conn = new SqlConnection(this.ConnectionString, new SqlCredential(this.DBUser, this.DBPassword)))
            {
                conn.Open();

                // Транзакция
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    // Команда
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.Transaction = transaction;
                        cmd.CommandType = CommandType.Text;

                        // Команда для вставки значений в таблицу
                        cmd.CommandText = $"INSERT INTO `{this.DatabaseTableName}` (`{this.DatabaseWordColumnName}`, `{this.DatabaseAmountColumnName}`) VALUES (@word, @amount);";
                        cmd.Parameters.Add(new SqlParameter("@word", SqlDbType.NChar));
                        cmd.Parameters.Add(new SqlParameter("@amount", SqlDbType.NChar));

                        try
                        {
                            // Добавить каждое значение статистики
                            foreach (var stat in data)
                            {
                                cmd.Parameters[0].Value = stat.Key;
                                cmd.Parameters[1].Value = stat.Value;
                                if (cmd.ExecuteNonQuery() != 1)
                                    throw new InvalidProgramException();
                            }

                            transaction.Commit();
                        }
                        catch (Exception)
                        {
                            // Роллбек при возникновении ошибки
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
        }
    }
}
