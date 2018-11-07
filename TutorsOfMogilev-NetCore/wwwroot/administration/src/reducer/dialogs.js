import {
  OPEN_DELETE_CONFIRM_DIALOG,
  CLOSE_DELETE_CONFIRM_DIALOG,
  OPEN_DIALOG_WITH_INPUT,
  CLOSE_DIALOG_WITH_INPUT,
  OPEN_ERROR_DIALOG,
  CLOSE_ERROR_DIALOG,
  SET_ERROR_MESSAGE,
  FAIL
} from '../constants';

const initialState = {
  deleteConfirmOpen: false,
  withInputOpen: false,
  errorOpen: false,
  errorMessage: 'Ошибка'
};

export default function dialogs(state = initialState, action) {
  const { type, error } = action;

  if (type.includes(FAIL))
    return {
      ...state,
      errorOpen: true,
      errorMessage: error.response.data.message
    };

  switch (type) {
    case OPEN_DELETE_CONFIRM_DIALOG:
      return { ...state, deleteConfirmOpen: true };

    case CLOSE_DELETE_CONFIRM_DIALOG:
      return { ...state, deleteConfirmOpen: false };

    case OPEN_DIALOG_WITH_INPUT:
      return { ...state, withInputOpen: true };

    case CLOSE_DIALOG_WITH_INPUT:
      return { ...state, withInputOpen: false };

    case OPEN_ERROR_DIALOG:
      return { ...state, errorOpen: true };

    case CLOSE_ERROR_DIALOG:
      return { ...state, errorOpen: false };

    case SET_ERROR_MESSAGE:
      return { ...state, errorMessage: error };

    default:
      return state;
  }
}
