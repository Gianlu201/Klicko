import { Bell, Settings } from 'lucide-react';
import React, { useState } from 'react';
import ToggleSwitch from '../ui/ToggleSwitch';

const SettingsComponent = () => {
  const [notificationPush, setNotificationPush] = useState(true);
  const [emailUpdate, setEmailUpdate] = useState(false);

  return (
    <>
      <h1 className='flex items-center justify-start gap-2 mb-10 text-2xl font-bold'>
        <Settings className='w-6 h-6' />
        Impostazioni
      </h1>

      <div className='my-2'>
        <div className='p-6 border shadow border-gray-400/40 rounded-xl'>
          <h2 className='flex items-center justify-start gap-2 mb-6 text-2xl font-bold'>
            <Bell className='w-6 h-6' />
            Notifiche
          </h2>

          <div className='flex items-center justify-between mb-4'>
            <p>Notifiche push</p>

            <ToggleSwitch
              value={notificationPush}
              onClick={() => {
                setNotificationPush(!notificationPush);
              }}
            />
          </div>

          <div className='flex items-center justify-between'>
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
