using ChurrascoApp.Interfaces;
using Microsoft.Win32.SafeHandles;
using SQLite;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Xamarin.Forms;

namespace ChurrascoApp.Database
{
    /// <summary>
    /// Classe responsável por todas as operações no banco de dados SQLite
    /// </summary>
    public class DataAccess : IDisposable
    {
        #region Variáveis

        private readonly SQLiteConnection _database;
        private bool _disposed = false;
        private SafeHandle _handle = new SafeFileHandle(IntPtr.Zero, true);

        #endregion

        #region Construtor

        /// <summary>
        /// Construtor
        /// </summary>
        public DataAccess()
        {
            _database = DependencyService.Get<ISQLiteConnection>().GetConnection();
        }

        #endregion

        #region Métodos

        #region Churrasco

        public List<DatabaseModels.Churrasco> GetChurrascos()
        {
            try
            {
                return _database.Table<DatabaseModels.Churrasco>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DatabaseModels.Churrasco GetChurrasco(int id)
        {
            try
            {
                return _database.Table<DatabaseModels.Churrasco>().Where(x => x.Id == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int InsertChurrasco(DatabaseModels.Churrasco churrasco)
        {
            try
            {
                var success = _database.Insert(churrasco);
                var newId = success.Equals(1) ? churrasco.Id : 0;

                return newId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetChurrascoCount()
        {
            try
            {
                return _database.Table<DatabaseModels.Churrasco>().Count();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region ChurrascoDetails

        public DatabaseModels.ChurrascoDetails GetChurrascoDetails(int idChurrasco)
        {
            try
            {
                return _database.Table<DatabaseModels.ChurrascoDetails>().Where(x => x.ChurrascoId == idChurrasco).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int InsertChurrascoDetails(DatabaseModels.ChurrascoDetails churrascoDetails)
        {
            try
            {
                var success = _database.Insert(churrascoDetails);
                var newId = success.Equals(1) ? churrascoDetails.Id : 0;

                return newId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int UpdateChurrascoDetails(DatabaseModels.ChurrascoDetails churrascoDetails)
        {
            try
            {
                return _database.Update(churrascoDetails);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        #endregion

        #region Participante

        public List<DatabaseModels.Participante> GetParticipantes()
        {
            try
            {
                return _database.Table<DatabaseModels.Participante>().OrderByDescending(x => x.DataCadastro).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<DatabaseModels.Participante> GetParticipantes(int idChurrasco)
        {
            try
            {
                return _database.Table<DatabaseModels.Participante>().Where(x => x.IdChurrasco == idChurrasco).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DatabaseModels.Participante GetParticipante(int id)
        {
            try
            {
                return _database.Table<DatabaseModels.Participante>().Where(x => x.Id == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int InsertParticipante(DatabaseModels.Participante participante)
        {
            try
            {
                var success = _database.Insert(participante);
                var newId = success.Equals(1) ? participante.Id : 0;

                return newId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int UpdateParticipante(DatabaseModels.Participante participante)
        {
            try
            {
                return _database.Update(participante);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int DeleteParticipante(DatabaseModels.Participante participante)
        {
            try
            {
                var result = _database.Delete<DatabaseModels.Participante>(participante.Id);

                _database.Commit();

                return result;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int GetParticipanteCount()
        {
            try
            {
                return _database.Table<DatabaseModels.Participante>().Count();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Funcionario

        public List<DatabaseModels.Funcionario> GetFuncionarios()
        {
            try
            {
                return _database.Table<DatabaseModels.Funcionario>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DatabaseModels.Funcionario GetFuncionario(int id)
        {
            try
            {
                return _database.Table<DatabaseModels.Funcionario>().Where(x => x.Id == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int InsertFuncionario(DatabaseModels.Funcionario funcionario)
        {
            try
            {
                var success = _database.Insert(funcionario);
                var newId = success.Equals(1) ? funcionario.Id : 0;

                return newId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int UpdateFuncionario(DatabaseModels.Funcionario funcionario)
        {
            try
            {
                return _database.Update(funcionario);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int DeleteFuncionario(DatabaseModels.Funcionario funcionario)
        {
            try
            {
                var result = _database.Delete<DatabaseModels.Funcionario>(funcionario.Id);

                _database.Commit();

                return result;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        #endregion

        #region Convidado

        public List<DatabaseModels.Convidado> GetConvidados()
        {
            try
            {
                return _database.Table<DatabaseModels.Convidado>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DatabaseModels.Convidado GetConvidado(int id)
        {
            try
            {
                return _database.Table<DatabaseModels.Convidado>().Where(x => x.Id == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int InsertConvidado(DatabaseModels.Convidado convidado)
        {
            try
            {
                var success = _database.Insert(convidado);
                var newId = success.Equals(1) ? convidado.Id : 0;

                return newId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int UpdateConvidado(DatabaseModels.Convidado convidado)
        {
            try
            {
                return _database.Update(convidado);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int DeleteConvidado(DatabaseModels.Convidado convidado)
        {
            try
            {
                var result = _database.Delete<DatabaseModels.Convidado>(convidado.Id);

                _database.Commit();

                return result;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        #endregion

        #region Gastos

        public List<DatabaseModels.Gasto> GetGastos(int idChurrasco)
        {
            try
            {
                return _database.Table<DatabaseModels.Gasto>().Where(x => x.IdChurrasco == idChurrasco).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<DatabaseModels.Gasto> GetGastosPorTipo(int idChurrasco, string tipo)
        {
            try
            {
                return _database.Table<DatabaseModels.Gasto>().Where(x => x.IdChurrasco == idChurrasco && x.Tipo == tipo).OrderByDescending(x => x.DataCompra).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DatabaseModels.Gasto GetGasto(int id)
        {
            try
            {
                return _database.Table<DatabaseModels.Gasto>().Where(x => x.Id == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int InsertGasto(DatabaseModels.Gasto gasto)
        {
            try
            {
                var success = _database.Insert(gasto);
                var newId = success.Equals(1) ? gasto.Id : 0;

                return newId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int UpdateGasto(DatabaseModels.Gasto gasto)
        {
            try
            {
                return _database.Update(gasto);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int DeleteGasto(DatabaseModels.Gasto gasto)
        {
            try
            {
                var result = _database.Delete<DatabaseModels.Gasto>(gasto.Id);

                _database.Commit();

                return result;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        #endregion

        #endregion

        #region Dispose

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _handle.Dispose();
            }

            _disposed = true;
        }

        #endregion
    }
}
