import { Funnel, Search } from 'lucide-react';
import React, { useEffect, useState } from 'react';
import Button from '../ui/Button';

const DashboardUsers = () => {
  const [users, setUsers] = useState([]);
  const [roles, setRoles] = useState([]);

  const getAllUsers = async () => {
    try {
      const response = await fetch('https://localhost:7235/api/Account', {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json',
        },
      });
      if (response.ok) {
        const data = await response.json();
        // console.log(data);

        console.log(data.accounts);

        setUsers(data.accounts);
      } else {
        throw new Error('Errore nel recupero dei dati!');
      }
    } catch {
      console.log('Error');
    }
  };

  const getAllRoles = async () => {
    try {
      const response = await fetch(
        'https://localhost:7235/api/Account/GetRoles',
        {
          method: 'GET',
          headers: {
            'Content-Type': 'application/json',
          },
        }
      );
      if (response.ok) {
        const data = await response.json();
        // console.log(data);

        // console.log(data.roles);

        setRoles(data.roles);
      } else {
        throw new Error('Errore nel recupero dei dati!');
      }
    } catch {
      console.log('Error');
    }
  };

  const editUserRole = async (userId, newRoleId) => {
    try {
      const response = await fetch(
        `https://localhost:7235/api/Account/EditRole/${userId}`,
        {
          method: 'PUT',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify(newRoleId),
        }
      );
      if (response.ok) {
        const data = await response.json();
        console.log(data);

        getAllUsers();

        // console.log(data.roles);

        // setRoles(data.roles);
      } else {
        throw new Error('Errore nel recupero dei dati!');
      }
    } catch {
      console.log('Error');
    }
  };

  useEffect(() => {
    getAllUsers();
    getAllRoles();
  }, []);

  return (
    <>
      <h2 className='text-2xl font-bold mb-2'>Gestione utenti</h2>
      <p className='text-gray-500 font-normal mb-6'>
        Visualizza e modifica i ruoli degli utenti
      </p>

      <div className='flex justify-between items-center gap-4 mb-8'>
        <div className='relative grow'>
          <input
            type='text'
            placeholder='Cerca utenti...'
            className='bg-background border border-gray-400/30 rounded-xl py-2 ps-10 w-full'
          />
          <Search className='absolute top-1/2 left-3 -translate-y-1/2 w-5 h-5 pb-0.5' />
        </div>

        <Button variant='outline' icon={<Funnel className='w-4 h-4' />}>
          Filtri
        </Button>
      </div>

      {/* tabella utenti registrati */}
      <div className='border border-gray-400/40 shadow-sm rounded-xl px-4 py-5'>
        {users.length > 0 ? (
          <div>
            <h3 className='text-xl font-semibold mb-2'>Utenti</h3>
            <p className='text-gray-500 font-normal text-sm mb-5'>
              {users.length} utenti trovati
            </p>

            <table className='w-full'>
              <thead>
                <tr className='grid grid-cols-24 gap-4 border-b border-gray-400/30 pb-3'>
                  <th className='col-span-6 text-gray-500 text-sm font-medium text-start ps-3'>
                    Nome
                  </th>
                  <th className='col-span-8 text-gray-500 text-sm font-medium text-start ps-3'>
                    Email
                  </th>
                  <th className='col-span-6 text-gray-500 text-sm font-medium text-start ps-3'>
                    Data registrazione
                  </th>
                  <th className='col-span-4 text-gray-500 text-sm font-medium text-start ps-3'>
                    Ruolo
                  </th>
                </tr>
              </thead>
              <tbody>
                {users.map((user) => (
                  <tr
                    key={user.userId}
                    className='grid grid-cols-24 gap-4 items-center hover:bg-gray-100 border-b border-gray-400/30 py-3 px-2 last-of-type:border-0 text-sm'
                  >
                    <td className='col-span-6'>
                      {user.firstName} {user.lastName}
                    </td>
                    <td className='col-span-6'>{user.email}</td>
                    <td className='col-span-6 text-end'>
                      {new Date(user.registrationDate).toLocaleDateString(
                        'it-IT',
                        {
                          year: 'numeric',
                          month: 'long',
                          day: 'numeric',
                        }
                      )}
                    </td>
                    <td className='col-span-6 text-center'>
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
  );
};

export default DashboardUsers;
