import {
  OPEN_DELETE_CONFIRM_DIALOG,
  CLOSE_DELETE_CONFIRM_DIALOG,
  OPEN_DIALOG_WITH_INPUT,
  CLOSE_DIALOG_WITH_INPUT,
  OPEN_ERROR_DIALOG,
  CLOSE_ERROR_DIALOG,
  SET_ERROR_MESSAGE
} from '../constants';

export const openDeleteConfirmDialog = () => ({
  type: OPEN_DELETE_CONFIRM_DIALOG
});

export const closeDeleteConfirmDialog = () => ({
  type: CLOSE_DELETE_CONFIRM_DIALOG
});

export const openDialogWithInput = () => ({
  type: OPEN_DIALOG_WITH_INPUT
});

export const closeDialogWithInput = () => ({
  type: CLOSE_DIALOG_WITH_INPUT
});

export const openErrorDialog = () => ({
  type: OPEN_ERROR_DIALOG
});

export const closeErrorDialog = () => ({
  type: CLOSE_ERROR_DIALOG
});

export const setErrorMessage = message => ({
  type: SET_ERROR_MESSAGE,
  message
});
