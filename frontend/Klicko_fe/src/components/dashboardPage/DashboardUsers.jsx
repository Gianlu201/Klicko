import { Funnel, Search } from 'lucide-react';
import React, { useEffect, useState } from 'react';
import Button from '../ui/Button';
import { useSelector } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import { toast } from 'sonner';

const DashboardUsers = () => {
  const backendUrl = import.meta.env.VITE_BACKEND_URL;

  const [isAuthorized, setIsAuthorized] = useState(false);

  const [users, setUsers] = useState([]);
  const [filteredUsers, setFilteredUsers] = useState([]);
  const [roles, setRoles] = useState([]);
  const [userSearch, setUserSearch] = useState('');

  const profile = useSelector((state) => {
    return state.profile;
  });

  const navigate = useNavigate();

  const checkAuthorization = () => {
    if (profile.role.toLowerCase() === 'admin') {
      setIsAuthorized(true);
    } else {
      navigate('/unauthorized');
    }
  };

  const getAllUsers = async () => {
    try {
      const response = await fetch(`${backendUrl}/Account`, {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json',
        },
      });
      if (response.ok) {
        const data = await response.json();

        setUsers(data.accounts);
        setFilteredUsers(data.accounts);
      } else {
        throw new Error('Errore nel recupero dei dati!');
      }
    } catch (e) {
      toast.error(
        <>
          <p className='font-bold'>Errore!</p>
          <p>{e.message}</p>
        </>
      );
    }
  };

  const getAllRoles = async () => {
    try {
      const response = await fetch(`${backendUrl}/Account/GetRoles`, {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json',
        },
      });
      if (response.ok) {
        const data = await response.json();

        setRoles(data.roles);
      } else {
        throw new Error('Errore nel recupero dei dati!');
      }
    } catch (e) {
      toast.error(
        <>
          <p className='font-bold'>Errore!</p>
          <p>{e.message}</p>
        </>
      );
    }
  };

  const editUserRole = async (userId, newRoleId) => {
    try {
      const response = await fetch(`${backendUrl}/Account/EditRole/${userId}`, {
        method: 'PUT',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(newRoleId),
      });
      if (response.ok) {
        toast.success(
          <>
            <p className='font-bold'>Modifica effettuata!</p>
            <p>Utente aggiornato con successo</p>
          </>
        );
        getAllUsers();
      } else {
        throw new Error(`Errore nella modifica dell'utente`);
      }
    } catch (e) {
      toast.error(
        <>
          <p className='font-bold'>Errore!</p>
          <p>{e.message}</p>
        </>
      );
    }
  };

  const findUsers = () => {
    const findUsers = [];

    console.log(users);

    users.forEach((user) => {
      if (
        user.firstName.toLowerCase().includes(userSearch.toLowerCase()) ||
        user.lastName.toLowerCase().includes(userSearch.toLowerCase()) ||
        user.email.toLowerCase().includes(userSearch.toLowerCase()) ||
        user.userRole.roleName.toLowerCase().includes(userSearch.toLowerCase())
      ) {
        findUsers.push(user);
      }
    });

    setFilteredUsers(findUsers);
  };

  useEffect(() => {
    if (profile.role) {
      checkAuthorization();

      if (isAuthorized) {
        getAllUsers();
        getAllRoles();
      }
    }
  }, [profile, isAuthorized]);

  useEffect(() => {
    findUsers();
  }, [userSearch]);

  return (
    <>
      {isAuthorized && (
        <>
          <h2 className='text-2xl font-bold mb-2'>Gestione utenti</h2>
          <p className='text-gray-500 font-normal mb-6'>
            Visualizza e modifica i ruoli degli utenti
          </p>

          <form className='relative grow mb-8'>
            <input
              type='text'
              placeholder='Cerca utenti...'
              className='bg-background border border-gray-400/30 rounded-xl py-2 ps-10 w-full'
              value={userSearch}
              onChange={(e) => {
                setUserSearch(e.target.value);
              }}
            />
            <Search className='absolute top-1/2 left-3 -translate-y-1/2 w-5 h-5 pb-0.5' />
          </form>

          {/* tabella utenti registrati */}
          <div className='border border-gray-400/40 shadow-sm rounded-xl px-4 py-5'>
            {filteredUsers.length > 0 ? (
              <div className='overflow-x-auto'>
                <h3 className='text-xl font-semibold mb-2'>Utenti</h3>
                <p className='text-gray-500 font-normal text-sm mb-5'>
                  {filteredUsers.length} utenti trovati
                </p>

                <table className='w-full min-w-sm'>
                  <thead>
                    <tr className='grid grid-cols-24 gap-4 border-b border-gray-400/30 pb-3'>
                      <th className='col-span-7 md:col-span-6 text-gray-500 text-sm font-medium text-start ps-3'>
                        Nome
                      </th>
                      <th className='col-span-11 md:col-span-8 text-gray-500 text-sm font-medium text-start ps-3'>
                        Email
                      </th>
                      <th className='hidden md:block col-span-6 text-gray-500 text-sm font-medium text-start ps-3'>
                        Data registrazione
                      </th>
                      <th className='col-span-6 md:col-span-4 text-gray-500 text-sm font-medium text-start md:text-center ps-3'>
                        Ruolo
                      </th>
                    </tr>
                  </thead>
                  <tbody>
                    {filteredUsers.map((user) => (
                      <tr
                        key={user.userId}
                        className='grid grid-cols-24 gap-4 items-center hover:bg-gray-100 border-b border-gray-400/30 py-3 last-of-type:border-0 text-sm'
                      >
                        <td className='col-span-7 md:col-span-6 overflow-hidden ps-2'>
                          {user.firstName} {user.lastName}
                        </td>
                        <td className='col-span-11 md:col-span-8 overflow-hidden max-md:text-xs max-md:font-medium'>
                          {user.email}
                        </td>
                        <td className='hidden md:block col-span-6 text-center'>
                          {new Date(user.registrationDate).toLocaleDateString(
                            'it-IT',
                            {
                              year: 'numeric',
                              month: 'long',
                              day: 'numeric',
                            }
                          )}
                        </td>
                        <td className='col-span-6 md:col-span-4 text-center pe-2'>
                          {roles.length > 0 ? (
                            <select
                              value={user.userRole.roleId}
                              onChange={(e) => {
                                editUserRole(user.userId, e.target.value);
                              }}
                            >
                              {roles.map((role) => (
                                <option value={role.roleId} key={role.roleId}>
                                  {role.roleName}
                                </option>
                              ))}
                            </select>
                          ) : (
                            user.userRole.roleName
                          )}
                        </td>
                      </tr>
                    ))}
                  </tbody>
                </table>
              </div>
            ) : (
              <div className='flex flex-col justify-center items-center gap-2 py-10'>
                <h3 className='text-xl font-semibold'>Nessun utente trovato</h3>
                <p className='text-gray-500 font-normal'>
                  Non è stato trovato nessun utente.
                </p>
              </div>
            )}
          </div>
        </>
      )}
    </>
  );
};

export default DashboardUsers;
