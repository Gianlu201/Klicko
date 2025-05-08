import { Funnel, Search } from 'lucide-react';
import React, { useEffect, useState } from 'react';
import Button from '../ui/Button';
import { useSelector } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import { toast } from 'sonner';
import SkeletonList from '../ui/SkeletonList';

const DashboardUsers = () => {
  const backendUrl = import.meta.env.VITE_BACKEND_URL;

  const [isAuthorized, setIsAuthorized] = useState(false);

  const [isLoading, setIsLoading] = useState(true);
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
      setIsLoading(true);

      const response = await fetch(`${backendUrl}/Account`, {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json',
        },
      });
      if (response.ok) {
        const data = await response.json();

        setIsLoading(false);
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
      setIsLoading(false);
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
          <h2 className='mb-2 text-2xl font-bold'>Gestione utenti</h2>
          <p className='mb-6 font-normal text-gray-500'>
            Visualizza e modifica i ruoli degli utenti
          </p>

          <form className='relative mb-8 grow'>
            <input
              type='text'
              placeholder='Cerca utenti...'
              className='w-full py-2 border bg-background border-gray-400/30 rounded-xl ps-10'
              value={userSearch}
              onChange={(e) => {
                setUserSearch(e.target.value);
              }}
            />
            <Search className='absolute top-1/2 left-3 -translate-y-1/2 w-5 h-5 pb-0.5' />
          </form>

          {/* tabella utenti registrati */}
          <div className='px-4 py-5 border shadow-sm border-gray-400/40 rounded-xl'>
            {isLoading ? (
              <SkeletonList />
            ) : filteredUsers.length > 0 ? (
              <div className='overflow-x-auto'>
                <h3 className='mb-2 text-xl font-semibold'>Utenti</h3>
                <p className='mb-5 text-sm font-normal text-gray-500'>
                  {filteredUsers.length} utenti trovati
                </p>

                <table className='w-full min-w-sm'>
                  <thead>
                    <tr className='grid gap-4 pb-3 border-b grid-cols-24 border-gray-400/30'>
                      <th className='col-span-7 text-sm font-medium text-gray-500 md:col-span-6 text-start ps-3'>
                        Nome
                      </th>
                      <th className='col-span-11 text-sm font-medium text-gray-500 md:col-span-8 text-start ps-3'>
                        Email
                      </th>
                      <th className='hidden col-span-6 text-sm font-medium text-gray-500 md:block text-start ps-3'>
                        Data registrazione
                      </th>
                      <th className='col-span-6 text-sm font-medium text-gray-500 md:col-span-4 text-start md:text-center ps-3'>
                        Ruolo
                      </th>
                    </tr>
                  </thead>
                  <tbody>
                    {filteredUsers.map((user) => (
                      <tr
                        key={user.userId}
                        className='grid items-center gap-4 py-3 text-sm border-b grid-cols-24 hover:bg-gray-100 border-gray-400/30 last-of-type:border-0'
                      >
                        <td className='col-span-7 overflow-hidden md:col-span-6 ps-2'>
                          {user.firstName} {user.lastName}
                        </td>
                        <td className='col-span-11 overflow-hidden md:col-span-8 max-md:text-xs max-md:font-medium'>
                          {user.email}
                        </td>
                        <td className='hidden col-span-6 text-center md:block'>
                          {new Date(user.registrationDate).toLocaleDateString(
                            'it-IT',
                            {
                              year: 'numeric',
                              month: 'long',
                              day: 'numeric',
                            }
                          )}
                        </td>
                        <td className='col-span-6 text-center md:col-span-4 pe-2'>
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
              <div className='flex flex-col items-center justify-center gap-2 py-10'>
                <h3 className='text-xl font-semibold'>Nessun utente trovato</h3>
                <p className='font-normal text-gray-500'>
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
