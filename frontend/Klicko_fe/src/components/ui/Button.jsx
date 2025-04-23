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
  const variants = {
    primary: 'bg-primary hover:bg-primary/80 text-white rounded-xl', // #19AEFF
    secondary: 'bg-secondary hover:bg-secondary/90 text-white rounded-xl', //#F97415
    success: 'bg-green-500 hover:bg-green-600 text-white rounded-xl',
    danger: 'bg-red-500 hover:bg-red-600 text-white rounded-xl',
    'danger-outline':
      'bg-transparent text-red-500 border border-red-500 rounded-xl hover:bg-red-100 hover:text-black',
    warning: 'bg-yellow-500 hover:bg-yellow-600 text-white rounded-xl',
    outline:
      'bg-background border border-black/30 text-black hover:bg-primary/80 hover:border-black/0 rounded-xl',
    ghost: 'bg-transparent hover:bg-gray-100 text-gray-700 rounded-xl',
    cat: 'bg-background text-primary border-gray-500 hover:bg-[#b8e1f7] hover:text-black rounded-xl',
    icon: 'bg-background border border-black/30 text-black hover:bg-primary/80 hover:border-black/0 rounded-full',
  };

  const sizes = {
    sm: 'py-1 px-3 text-sm',
    md: 'py-2 px-4 text-base',
    lg: 'py-3 px-6 text-lg',
    icon: 'p-2',
  };

  const width = fullWidth ? 'w-full' : '';

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
        font-medium transition-colors duration-200 focus:outline-none focus:ring-2 focus:ring-blue-300
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
