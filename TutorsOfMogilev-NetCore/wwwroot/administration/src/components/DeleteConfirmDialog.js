import React from 'react';
import Button from '@material-ui/core/Button';
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogTitle from '@material-ui/core/DialogTitle';

export default function DeleteConfirmDialog({
  isOpen,
  handleClose,
  handleConfirm
}) {
  const onConfirm = () => {
    handleConfirm();
    handleClose();
  };

  return (
    <Dialog open={isOpen} onClose={handleClose}>
      <DialogTitle>Подтвердите удаление</DialogTitle>
      <DialogActions>
        <Button onClick={handleClose} color="primary">
          Отмена
        </Button>
        <Button onClick={onConfirm} color="primary">
          Подтверждаю
        </Button>
      </DialogActions>
    </Dialog>
  );
}
