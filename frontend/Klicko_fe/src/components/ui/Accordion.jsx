import { useState } from 'react';
import { ChevronDown } from 'lucide-react';

export default function Accordion({ intestation, children }) {
  const [isOpen, setIsOpen] = useState(false);

  return (
    <div className='w-full border-b border-gray-400/40'>
      <button
        onClick={() => setIsOpen(!isOpen)}
        className='flex w-full items-center justify-between gap-2 px-4 py-3 text-left hover:bg-gray-50 transition'
      >
        <div className='w-full'>{intestation}</div>
        <ChevronDown
          className={`h-4 w-4 transform transition-transform duration-300 ${
            isOpen ? 'rotate-180' : ''
          }`}
        />
      </button>
      <div
        className={`overflow-hidden ${
          isOpen ? 'max-h-96 p-4 pt-0' : 'max-h-0 p-0'
        }`}
      >
        <div>{children}</div>
      </div>
    </div>
  );
}
