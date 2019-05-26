import { CLOSE_ERROR_DIALOG, FAIL } from '../constants';

const initialState = {
  opened: false,
  errorMessage: 'Ошибка'
};

const errorDialog = (state = initialState, action) => {
  const { type, error } = action;

  if (type.includes(FAIL))
    return {
      opened: true,
      errorMessage: error && error.response ? error.response.data : 'Ошибка'
    };

  switch (type) {
    case CLOSE_ERROR_DIALOG:
      return { ...state, opened: false };

    default:
      return state;
  }
};

export default errorDialog;
