import { X } from 'lucide-react';
import React, { useEffect, useState } from 'react';

const ImagesPreview = ({
  coverImage = null,
  images,
  onItemRemove,
  initialState,
}) => {
  const [prevImages, setPrevImages] = useState(null);
  const [prevCoverImage, setPrevCoverImage] = useState(null);
  const [imagesRemoved, setImagesRemoved] = useState([]);

  const handleRemove = (imgId) => {
    let index = -1;

    if (coverImage === null) {
      const nextImages = prevImages;

      prevImages.forEach((img, i) => {
        if (img.imageId === imgId) {
          index = i;
        }
      });

      nextImages.splice(index, 1);
      setPrevImages(nextImages);

      setImagesRemoved((prevState) => [...prevState, imgId]);
    } else {
      setPrevCoverImage(null);
      setImagesRemoved(true);
      onItemRemove(true);
    }
  };

  useEffect(() => {
    if (coverImage === null) {
      if (prevImages === null) {
        setPrevImages(images);
      }
    } else if (prevCoverImage === null && imagesRemoved !== true) {
      setPrevCoverImage(coverImage);
    }

    onItemRemove(imagesRemoved);
  }, [imagesRemoved]);

  useEffect(() => {
    onItemRemove(initialState);
  }, []);

  return (
    <div className='w-full'>
      {prevImages !== null && images && images.length > 0 && (
        <>
          <p>Immagini attualmente presenti:</p>

          <div className='grid grid-cols-2 sm:grid-cols-3 md:grid-cols-4 gap-4 mt-4'>
            {images.map((img, idx) => (
              <div
                key={idx}
                className='relative w-full h-28 rounded-lg overflow-hidden border border-gray-200 shadow-sm group'
              >
                <img
                  src={`https://klicko-backend-api.azurewebsites.net/uploads/${img.url}`}
                  alt={`preview-${idx}`}
                  className='object-cover w-full h-full'
                />
                <button
                  type='button'
                  onClick={() => handleRemove(img.imageId)}
                  className='absolute top-1 right-1 justify-center items-center bg-red-500 text-white rounded-full p-1 text-xs w-fit aspect-square cursor-pointer hover:opacity-100 hover:bg-red-600 transition'
                >
                  <X className='text-white font-bold w-4 h-4' />
                </button>
              </div>
            ))}
          </div>
        </>
      )}

      {prevCoverImage !== null && (
        <>
          <p>Immagini attualmente presenti:</p>

          <div className='grid grid-cols-2 sm:grid-cols-3 md:grid-cols-4 gap-4 mt-4'>
            <div className='relative w-full h-28 rounded-lg overflow-hidden border border-gray-200 shadow-sm group'>
              <img
                src={`https://klicko-backend-api.azurewebsites.net/uploads/${prevCoverImage}`}
                className='object-cover w-full h-full'
              />
              <button
                type='button'
                onClick={() => handleRemove(prevCoverImage)}
                className='absolute top-1 right-1 justify-center items-center bg-red-500 text-white rounded-full p-1 text-xs w-fit aspect-square cursor-pointer hover:opacity-100 hover:bg-red-600 transition'
              >
                <X className='text-white font-bold w-4 h-4' />
              </button>
            </div>
          </div>
        </>
      )}
    </div>
  );
};

export default ImagesPreview;
