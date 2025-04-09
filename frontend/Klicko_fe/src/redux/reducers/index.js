const initialState = {
  profile: {},
};

const mainReducer = (state = initialState, action) => {
  switch (action.type) {
    case 'A':
      return {
        ...state,
      };

    default:
      return state;
  }
};

export default mainReducer;
