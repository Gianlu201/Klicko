const AddressFormComponent = ({
  name,
  setName,
  surname,
  setSurname,
  city,
  setCity,
  address,
  setAddress,
  cap,
  setCap,
  showError = false,
}) => {
  return (
    <form>
      <div className='grid items-start grid-cols-2 gap-10'>
        <div className='flex flex-col gap-2 my-3'>
          <label className='text-sm font-medium'>Nome</label>
          <input
            type='text'
            className='bg-background border border-gray-400/40 rounded-lg px-3 py-1.5'
            placeholder='Nome'
            value={name}
            onChange={(e) => {
              setName(e.target.value);
            }}
          />
          {showError && name.trim().length === 0 && (
            <span className='text-sm text-red-600'>Campo obbligatorio!</span>
          )}
        </div>

        <div className='flex flex-col gap-2 my-3'>
          <label className='text-sm font-medium'>Cognome</label>
          <input
            type='text'
            className='bg-background border border-gray-400/40 rounded-lg px-3 py-1.5'
            placeholder='Cognome'
            value={surname}
            onChange={(e) => {
              setSurname(e.target.value);
            }}
          />
          {showError && surname.trim().length === 0 && (
            <span className='text-sm text-red-600'>Campo obbligatorio!</span>
          )}
        </div>
      </div>

      <div className='grid items-start grid-cols-2 gap-10'>
        <div className='flex flex-col gap-2 my-3'>
          <label className='text-sm font-medium'>Città</label>
          <input
            type='text'
            className='bg-background border border-gray-400/40 rounded-lg px-3 py-1.5'
            placeholder='Città'
            value={city}
            onChange={(e) => {
              setCity(e.target.value);
            }}
          />
          {showError && city.trim().length === 0 && (
            <span className='text-sm text-red-600'>Campo obbligatorio!</span>
          )}
        </div>

        <div className='flex flex-col gap-2 my-3'>
          <label className='text-sm font-medium'>CAP</label>
          <input
            type='text'
            className='bg-background border border-gray-400/40 rounded-lg px-3 py-1.5'
            placeholder='CAP'
            value={cap}
            onChange={(e) => {
              setCap(e.target.value);
            }}
          />
          {showError && cap.trim().length === 0 && (
            <span className='text-sm text-red-600'>Campo obbligatorio!</span>
          )}
        </div>
      </div>

      <div className='flex flex-col gap-2 my-3'>
        <label className='text-sm font-medium'>Indirizzo</label>
        <input
          type='text'
          className='bg-background border border-gray-400/40 rounded-lg px-3 py-1.5'
          placeholder='Indirizzo'
          value={address}
          onChange={(e) => {
            setAddress(e.target.value);
          }}
        />
        {showError && address.trim().length === 0 && (
          <span className='text-sm text-red-600'>Campo obbligatorio!</span>
        )}
      </div>
    </form>
  );
};

export default AddressFormComponent;
