#region Usings
using Dominio.DTOs;
using Dominio.Interfaces.Repositorios;
using MediatR;
#endregion

namespace Aplicacion.Mediators.Usuarios
{
    public class UsuarioListar
    {
        #region Request
        public class UsuarioListarCargar : IRequest<IReadOnlyList<UsuariosCompactoDTO>> { }
        #endregion

        public class Manejador : IRequestHandler<UsuarioListarCargar, IReadOnlyList<UsuariosCompactoDTO>>
        {
            #region Dependencias
            private readonly IUsuariosRepository _usuariosRepository;
            #endregion

            #region Constructor
            public Manejador(IUsuariosRepository usuariosRepository)
            {
                _usuariosRepository = usuariosRepository ?? throw new ArgumentNullException(nameof(usuariosRepository));
            }
            #endregion

            #region Métodos
            public async Task<IReadOnlyList<UsuariosCompactoDTO>> Handle(UsuarioListarCargar request, CancellationToken cancellationToken)
            {
                var resultado = await _usuariosRepository.HttpInfoCliente().ConfigureAwait(false);
                if(resultado.data!.Any())
                {
                    List<UsuariosCompactoDTO> resultadoFinal = new();

                    foreach(var registro in resultado.data!)
                    {
                        UsuariosCompactoDTO linea = new()
                        {
                            id = registro.id,
                            nombre = registro.first_name + " " + registro.last_name
                        };

                        resultadoFinal.Add(linea);
                    }
                    return resultadoFinal!;
                }
                else
                {
                    List<UsuariosCompactoDTO> lista = new();
                    return lista;
                }
            }
            #endregion
        }
    }
}