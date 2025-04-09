import React from 'react';

const Button = ({
  children,
  onClick,
  className = '',
  variant = 'primary',
  size = 'md',
  disabled = false,
  type = 'button',
  fullWidth = false,
  icon = null,
  iconPosition = 'left',
}) => {
  // Varianti di stile
  const variants = {
    primary: 'bg-[#19AEFF] hover:bg-[#19AEFF]/80 text-white', // #19AEFF
    secondary: 'bg-[#F97415] hover:bg-gray-300 text-white', //#F97415
    success: 'bg-green-500 hover:bg-green-600 text-white',
    danger: 'bg-red-500 hover:bg-red-600 text-white',
    warning: 'bg-yellow-500 hover:bg-yellow-600 text-white',
    outline:
      'bg-transparent border border-black/30 text-black hover:bg-[#19AEFF]/60 hover:border-black/0',
    ghost: 'bg-transparent hover:bg-gray-100 text-gray-700',
  };

  // Dimensioni
  const sizes = {
    sm: 'py-1 px-3 text-sm',
    md: 'py-2 px-4 text-base',
    lg: 'py-3 px-6 text-lg',
  };

  // Gestione larghezza piena
  const width = fullWidth ? 'w-full' : '';

  // Gestione disabilitato
  const disabledStyles = disabled
    ? 'opacity-50 cursor-not-allowed'
    : 'cursor-pointer';

  return (
    <button
      type={type}
      className={`
        ${variants[variant]} 
        ${sizes[size]} 
        ${width} 
        ${disabledStyles}
        rounded-xl font-medium transition-colors duration-200 focus:outline-none focus:ring-2 focus:ring-blue-300
        flex items-center justify-center
        ${className}
      `}
      onClick={onClick}
      disabled={disabled}
    >
      {icon && iconPosition === 'left' && <span className='mr-2'>{icon}</span>}

      {children}

      {icon && iconPosition === 'right' && <span className='ml-2'>{icon}</span>}
    </button>
  );
};

export default Button;
