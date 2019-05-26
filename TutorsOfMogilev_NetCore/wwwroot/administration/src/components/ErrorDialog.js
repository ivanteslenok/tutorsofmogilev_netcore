import React from 'react';
import Button from '@material-ui/core/Button';
import Dialog from '@material-ui/core/Dialog';
import DialogContent from '@material-ui/core/DialogContent';
import DialogContentText from '@material-ui/core/DialogContentText';
import DialogActions from '@material-ui/core/DialogActions';
import DialogTitle from '@material-ui/core/DialogTitle';

const ErrorDialog = ({ isOpen, handleClose, errorMessage }) => (
  <Dialog open={isOpen} onClose={handleClose}>
    <DialogTitle>Упс! Произошла ошибка :{'('}</DialogTitle>
    <DialogContent>
      <DialogContentText style={{ color: '#F44336' }}>{errorMessage}</DialogContentText>
    </DialogContent>
    <DialogActions>
      <Button onClick={handleClose} color="primary">
        Закрыть
      </Button>
    </DialogActions>
  </Dialog>
);

export default ErrorDialog;
