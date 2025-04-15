// import { useState } from 'react';

export default function ToggleSwitch({ value, onClick }) {
  // const [enabled, setEnabled] = useState(value);

  return (
    <button
      type='button'
      onClick={onClick}
      className={`w-full h-6 flex items-center rounded-full p-1 transition-colors duration-300 ${
        value ? 'bg-primary' : 'bg-gray-300'
      }`}
    >
      <div
        className={`bg-white w-4 h-4 rounded-full shadow-md transform transition-transform duration-300 ${
          value ? 'translate-x-6' : 'translate-x-0'
        }`}
      ></div>
    </button>
  );
}
