import { useEffect, useState } from 'react';
import { ChevronLeft, ChevronRight, X } from 'lucide-react';
import {
  Button,
  Modal,
  ModalBody,
  ModalFooter,
  ModalHeader,
} from 'flowbite-react';

export default function Carousel({
  items = [],
  slidesVisible = 3,
  autoplay = true,
  delay = 5000,
  className = '',
}) {
  const [current, setCurrent] = useState(0);
  const [openModal, setOpenModal] = useState(false);
  const [selectedImgUrl, setSelectedImgUrl] = useState('');

  useEffect(() => {
    if (!autoplay) return;
    const timer = setInterval(() => {
      setCurrent((prev) => (prev + 1) % items.length);
    }, delay);
    return () => clearInterval(timer);
  }, [autoplay, delay, items.length]);

  const nextSlide = () => {
    setCurrent((prev) => (prev + 1) % items.length);
  };

  const prevSlide = () => {
    setCurrent((prev) => (prev - 1 + items.length) % items.length);
  };

  return (
    <div
      className={`relative w-full overflow-hidden rounded-2xl shadow-lg ${className}`}
    >
      <div
        className='flex gap-4 transition-transform duration-700 ease-in-out py-6 px-4'
        style={{
          width: `${(items.length / slidesVisible) * 100}%`,
          transform: `translateX(-${(100 / items.length) * current}%)`,
        }}
      >
        {items.map((item, index) => (
          <div key={index}>
            <img
              src={`https://klicko-backend-api.azurewebsites.net/uploads/${item.url}`}
              className='w-full h-full rounded-2xl cursor-pointer'
              onClick={() => {
                setSelectedImgUrl(item.url);
                setOpenModal(true);
              }}
            />
          </div>
        ))}
      </div>

      <button
        onClick={prevSlide}
        className='absolute left-4 top-1/2 -translate-y-1/2 bg-white/80 hover:bg-white p-2 rounded-full shadow transition'
      >
        <ChevronLeft className='w-5 h-5 text-gray-700 cursor-pointer' />
      </button>
      <button
        onClick={nextSlide}
        className='absolute right-4 top-1/2 -translate-y-1/2 bg-white/80 hover:bg-white p-2 rounded-full shadow transition'
      >
        <ChevronRight className='w-5 h-5 text-gray-700 cursor-pointer' />
      </button>

      {/* Modale */}
      <Modal dismissible show={openModal} onClose={() => setOpenModal(false)}>
        <X
          className='absolute top-4 right-4 z-50 text-white w-6 h-6 p-1 bg-black/60 rounded-full cursor-pointer'
          onClick={() => setOpenModal(false)}
        />
        <img
          src={`https://klicko-backend-api.azurewebsites.net/uploads/${selectedImgUrl}`}
          className='w-full h-full rounded-2xl'
        />
      </Modal>
    </div>
  );
}
