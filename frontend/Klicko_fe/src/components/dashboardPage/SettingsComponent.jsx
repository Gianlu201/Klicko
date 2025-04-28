import { Bell, Settings } from 'lucide-react';
import React, { useState } from 'react';
import ToggleSwitch from '../ui/ToggleSwitch';

const SettingsComponent = () => {
  const [notificationPush, setNotificationPush] = useState(true);
  const [emailUpdate, setEmailUpdate] = useState(false);

  return (
    <>
      <h1 className='text-2xl font-bold flex justify-start items-center gap-2 mb-10'>
        <Settings className='w-6 h-6' />
        Impostazioni
      </h1>

      <div className='my-2'>
        <div className='border border-gray-400/40 rounded-xl shadow p-6'>
          <h2 className='text-2xl font-bold flex justify-start items-center gap-2 mb-6'>
            <Bell className='w-6 h-6' />
            Notifiche
          </h2>

          <div className='flex justify-between items-center mb-4'>
            <p>Notifiche push</p>

            <ToggleSwitch
              value={notificationPush}
              onClick={() => {
                setNotificationPush(!notificationPush);
              }}
            />
          </div>

          <div className='flex justify-between items-center'>
            <p>Aggiornamenti via email</p>

            <ToggleSwitch
              value={emailUpdate}
              onClick={() => {
                setEmailUpdate(!emailUpdate);
              }}
            />
          </div>
        </div>
      </div>
    </>
  );
};

export default SettingsComponent;
