import { Upload, X } from 'lucide-react';
import React, { useRef, useState, useEffect } from 'react';

let counter = 0;

// onFileSelected è il metodo che sarà chiamato quando un'immagine viene selezionata e caricata

const UploadFile = ({ onFilesSelected, multiple = false }) => {
  const inputRef = useRef(null);
  const [isDragging, setIsDragging] = useState(false);
  const [files, setFiles] = useState([]); // File veri
  const [previews, setPreviews] = useState([]); // { file, url }

  const handleFiles = (selectedFiles) => {
    if (multiple) {
      const filesArray = Array.from(selectedFiles);

      const newPreviews = filesArray.map((file) => {
        counter++;
        return {
          id: counter,
          file,
          url: URL.createObjectURL(file),
        };
      });

      const updated = [...files, ...filesArray];
      const updatedPreviews = [...previews, ...newPreviews];

      setFiles(updated);
      setPreviews(updatedPreviews);
      onFilesSelected(updated);
    } else {
      const newImg = {
        id: 0,
        selectedFiles,
        url: URL.createObjectURL(selectedFiles),
      };

      setFiles(selectedFiles);
      setPreviews([newImg]);
      onFilesSelected(selectedFiles);
    }
  };

  const handleRemove = (id) => {
    if (multiple) {
      const updatedFiles = [...files];
      const updatedPreviews = [...previews];

      let index = 0;
      // Revoke blob URL
      updatedPreviews.forEach((element, i) => {
        if (element.id === id) {
          index = i;
          URL.revokeObjectURL(element.url);
        }
      });

      updatedFiles.splice(index, 1);
      updatedPreviews.splice(index, 1);

      setFiles(updatedFiles);
      setPreviews(updatedPreviews);
      onFilesSelected(updatedFiles);
    } else {
      setFiles(null);
      setPreviews(null);
      onFilesSelected(null);
    }
  };

  const handleDragOver = (e) => {
    e.preventDefault();
    setIsDragging(true);
  };

  const handleDragLeave = () => {
    setIsDragging(false);
  };

  const handleDrop = (e) => {
    e.preventDefault();
    setIsDragging(false);
    if (e.dataTransfer.files && e.dataTransfer.files.length > 0 && multiple) {
      handleFiles(e.dataTransfer.files);
      e.dataTransfer.clearData();
    }
    if (e.dataTransfer.files && e.dataTransfer.files.length > 0 && !multiple) {
      handleFiles(e.dataTransfer.files[0]);
      e.dataTransfer.clearData();
    }
  };

  const handleInputChange = (e) => {
    if (multiple) {
      handleFiles(e.target.files);
    } else {
      handleFiles(e.target.files[0]);
    }
  };

  useEffect(() => {
    return () => {
      previews.forEach((p) => URL.revokeObjectURL(p.url));
    };
  }, []);

  return (
    <div className='w-full'>
      <label
        onDragOver={handleDragOver}
        onDragLeave={handleDragLeave}
        onDrop={handleDrop}
        className={`flex flex-col items-center justify-center w-full h-40 p-4 border-2 border-dashed ${
          isDragging ? 'border-primary bg-blue-50' : 'border-primary/70'
        } rounded-2xl cursor-pointer transition`}
      >
        <input
          type='file'
          accept='image/*'
          multiple={multiple}
          ref={inputRef}
          className='hidden'
          onChange={handleInputChange}
        />

        <Upload className='w-10 h-10 mb-2 text-primary' />

        <p className='text-sm font-medium text-primary'>
          {multiple
            ? 'Trascina qui i file o clicca per selezionare'
            : 'Trascina qui il file o clicca per selezionare'}
        </p>
        <p className='text-xs text-gray-400'>
          {multiple ? '(Immagini multiple)' : '(Immagine singola)'}
        </p>
      </label>

      {previews !== null && previews.length > 0 && (
        <div className='grid grid-cols-2 gap-4 mt-4 sm:grid-cols-3 md:grid-cols-4'>
          {previews.map((p, idx) => (
            <div
              key={idx}
              className='relative w-full overflow-hidden border border-gray-200 rounded-lg shadow-sm h-28 group'
            >
              <img
                src={p.url}
                alt={`preview-${idx}`}
                className='object-cover w-full h-full'
              />
              <button
                type='button'
                onClick={() => handleRemove(p.id)}
                className='absolute items-center justify-center p-1 text-xs text-white transition bg-red-500 rounded-full cursor-pointer top-1 right-1 w-fit aspect-square hover:opacity-100 hover:bg-red-600'
              >
                <X className='w-4 h-4 font-bold text-white' />
              </button>
            </div>
          ))}
        </div>
      )}
    </div>
  );
};

export default UploadFile;
