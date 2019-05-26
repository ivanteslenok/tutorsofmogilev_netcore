import { connect } from 'react-redux';
import { CLOSE_ERROR_DIALOG } from '../constants';
import ErrorDialog from '../components/ErrorDialog';

const mapStateToProps = state => {
  return {
    isOpen: state.errorDialog.opened,
    errorMessage: state.errorDialog.errorMessage
  };
};

const mapDispatchToProps = dispatch => {
  return {
    handleClose: () => {
      dispatch({ type: CLOSE_ERROR_DIALOG });
    }
  };
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(ErrorDialog);
