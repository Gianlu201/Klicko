import { useEffect, useRef } from 'react';
// eslint-disable-next-line no-unused-vars
import { motion, AnimatePresence } from 'framer-motion';
import Button from './Button';

export default function BottomBanner({
  isOpen,
  onClose,
  children,
  theme = 'light',
}) {
  const overlayRef = useRef(null);

  useEffect(() => {
    function handleClickOutside(event) {
      if (overlayRef.current && event.target === overlayRef.current) {
        onClose();
      }
    }
    document.addEventListener('mousedown', handleClickOutside);
    return () => document.removeEventListener('mousedown', handleClickOutside);
  }, [onClose]);

  useEffect(() => {
    if (isOpen) {
      document.body.style.overflow = 'hidden';
    } else {
      document.body.style.overflow = '';
    }
    return () => {
      document.body.style.overflow = '';
    };
  }, [isOpen]);

  const themes = {
    light: 'bg-white text-gray-800',
    dark: 'bg-gray-800 text-white',
    blue: 'bg-blue-600 text-white',
  };

  return (
    <AnimatePresence>
      {isOpen && (
        <div ref={overlayRef} className='fixed inset-0 z-50 bg-black/40'>
          <motion.div
            className={`absolute bottom-0 left-0 right-0 rounded-t-2xl shadow-xl p-4 h-[70vh] flex flex-col items-center justify-start overflow-y-auto ${themes[theme]}`}
            initial={{ y: '100%' }}
            animate={{ y: 0 }}
            exit={{ y: '100%' }}
            transition={{ type: 'spring', stiffness: 300, damping: 30 }}
            drag='y'
            dragConstraints={{ top: 0, bottom: 0 }}
            onDragEnd={(event, info) => {
              if (info.offset.y > 100 || info.velocity.y > 500) {
                onClose();
              }
            }}
          >
            {/* Drag Handle */}
            <div className='flex justify-center'>
              <div className='w-12 h-1.5 bg-gray-400 rounded-full mt-2'></div>
            </div>

            <div className='px-4 my-4 overflow-y-auto w-md md:w-xl lg:w-3xl grow'>
              {children}
              <Button
                variant='outline'
                fullWidth={true}
                onClick={onClose}
                className='mt-10'
              >
                Chiudi
              </Button>
            </div>
            <div className='w-full'></div>
          </motion.div>
        </div>
      )}
    </AnimatePresence>
  );
}
