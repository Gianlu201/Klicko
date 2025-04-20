import React, { useState, useRef, useEffect } from 'react';

const Dropdown = ({
  trigger,
  children,
  align = 'left',
  width = 'w-48',
  className = '',
  closeOnSelect = true,
  closeOnClickOutside = true,
  disabled = false,
}) => {
  const [isOpen, setIsOpen] = useState(false);
  const dropdownRef = useRef(null);

  // Gestione del click esterno per chiudere il dropdown
  useEffect(() => {
    if (!closeOnClickOutside) return;

    const handleClickOutside = (event) => {
      if (dropdownRef.current && !dropdownRef.current.contains(event.target)) {
        setIsOpen(false);
      }
    };

    if (isOpen) {
      document.addEventListener('mousedown', handleClickOutside);
    }

    return () => {
      document.removeEventListener('mousedown', handleClickOutside);
    };
  }, [isOpen, closeOnClickOutside]);

  // Gestione della chiusura quando viene selezionato un elemento
  const handleItemClick = (callback) => {
    return (e) => {
      if (callback) callback(e);
      if (closeOnSelect) setIsOpen(false);
    };
  };

  // Modifica gli elementi figli per aggiungere il comportamento di chiusura
  const modifiedChildren = React.Children.map(children, (child) => {
    if (React.isValidElement(child) && child.props.onClick) {
      return React.cloneElement(child, {
        onClick: handleItemClick(child.props.onClick),
      });
    }
    return child;
  });

  // Posizionamento del menu a seconda dell'allineamento
  const alignmentClasses = {
    left: 'left-0',
    right: 'right-0',
    center: 'left-1/2 -translate-x-1/2',
  };

  return (
    <div className={`relative inline-block ${className}`} ref={dropdownRef}>
      {/* Elemento trigger del dropdown */}
      <div
        onClick={() => !disabled && setIsOpen(!isOpen)}
        className={
          disabled ? 'cursor-not-allowed opacity-70' : 'cursor-pointer'
        }
      >
        {trigger}
      </div>

      {/* Menu del dropdown */}
      {isOpen && (
        <div
          className={`absolute z-50 mt-2 ${width} ${alignmentClasses[align]} rounded-md shadow-lg bg-white ring-1 ring-black ring-opacity-5 focus:outline-none py-1`}
        >
          {modifiedChildren}
        </div>
      )}
    </div>
  );
};

// Componente per gli elementi del dropdown
const DropdownItem = ({
  children,
  onClick,
  className = '',
  icon = null,
  disabled = false,
  danger = false,
  divider = false,
}) => {
  // Stili condizionali
  const baseClasses = 'flex items-center w-full px-4 py-2 text-sm';
  const stateClasses = disabled
    ? 'opacity-50 cursor-not-allowed text-gray-500'
    : 'hover:bg-gray-100 rounded-xl duration-150 ease-in-out cursor-pointer';
  const textColor = danger ? 'text-red-600' : 'text-gray-700';

  // Se è un divisore, mostra solo una linea
  if (divider) {
    return <hr className='my-1 border-gray-200' />;
  }

  return (
    <button
      type='button'
      className={`${baseClasses} ${stateClasses} ${textColor} ${className}`}
      onClick={disabled ? undefined : onClick}
      disabled={disabled}
    >
      {icon && <span className='mr-2'>{icon}</span>}
      {children}
    </button>
  );
};

// Componente per l'intestazione del dropdown
const DropdownHeader = ({ children, className = '' }) => {
  return (
    <div
      className={`px-4 py-2 text-xs font-semibold text-gray-500 uppercase tracking-wider ${className}`}
    >
      {children}
    </div>
  );
};

export { Dropdown, DropdownItem, DropdownHeader };
