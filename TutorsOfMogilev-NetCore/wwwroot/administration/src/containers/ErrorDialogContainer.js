import { connect } from 'react-redux';
import ErrorDialog from '../components/ErrorDialog';
import * as dialogsAC from '../AC/dialogs';

const mapStateToProps = state => {
  return {
    isOpen: state.dialogs.errorOpen,
    errorMessage: state.dialogs.errorMessage
  };
};

const mapDispatchToProps = dispatch => {
  return {
    handleClose: () => {
      dispatch(dialogsAC.closeErrorDialog());
    }
  };
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(ErrorDialog);
