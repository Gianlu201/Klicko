const initialState = {
  profile: {},
};

const mainReducer = (state = initialState, action) => {
  switch (action.type) {
    case 'SET_LOGGED_USER':
      return {
        ...state,
        profile: action.payload,
      };

    case 'LOGOUT':
      return {
        ...state,
        profile: {},
      };

    default:
      return state;
  }
};

export default mainReducer;
