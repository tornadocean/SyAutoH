// **********************************************************************
//
// Copyright (c) 2003-2011 ZeroC, Inc. All rights reserved.
//
// This copy of Ice is licensed to you under the terms described in the
// ICE_LICENSE file included in this distribution.
//
// **********************************************************************
//
// Ice version 3.4.2
//
// <auto-generated>
//
// Generated from file `iFoupMove.ice'
//
// Warning: do not edit this file.
//
// </auto-generated>
//


using _System = global::System;
using _Microsoft = global::Microsoft;

#pragma warning disable 1591

namespace MCS
{
    [_System.Runtime.InteropServices.ComVisible(false)]
    [_System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704")]
    [_System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707")]
    [_System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709")]
    [_System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710")]
    [_System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1711")]
    [_System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1715")]
    [_System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716")]
    [_System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720")]
    [_System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1722")]
    [_System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1724")]
    public partial interface FoupMove : Ice.Object, FoupMoveOperations_, FoupMoveOperationsNC_
    {
    }
}

namespace MCS
{
    [_System.CodeDom.Compiler.GeneratedCodeAttribute("slice2cs", "3.4.2")]
    public delegate void Callback_FoupMove_Move(int ret__);
}

namespace MCS
{
    [_System.CodeDom.Compiler.GeneratedCodeAttribute("slice2cs", "3.4.2")]
    public interface FoupMovePrx : Ice.ObjectPrx
    {
        int Move(int FoupID, int From, int To);
        int Move(int FoupID, int From, int To, _System.Collections.Generic.Dictionary<string, string> context__);

        Ice.AsyncResult<MCS.Callback_FoupMove_Move> begin_Move(int FoupID, int From, int To);
        Ice.AsyncResult<MCS.Callback_FoupMove_Move> begin_Move(int FoupID, int From, int To, _System.Collections.Generic.Dictionary<string, string> ctx__);

        Ice.AsyncResult begin_Move(int FoupID, int From, int To, Ice.AsyncCallback cb__, object cookie__);
        Ice.AsyncResult begin_Move(int FoupID, int From, int To, _System.Collections.Generic.Dictionary<string, string> ctx__, Ice.AsyncCallback cb__, object cookie__);

        int end_Move(Ice.AsyncResult r__);
    }
}

namespace MCS
{
    [_System.CodeDom.Compiler.GeneratedCodeAttribute("slice2cs", "3.4.2")]
    public interface FoupMoveOperations_
    {
        int Move(int FoupID, int From, int To, Ice.Current current__);
    }

    [_System.CodeDom.Compiler.GeneratedCodeAttribute("slice2cs", "3.4.2")]
    public interface FoupMoveOperationsNC_
    {
        int Move(int FoupID, int From, int To);
    }
}

namespace MCS
{
    [_System.Runtime.InteropServices.ComVisible(false)]
    [_System.CodeDom.Compiler.GeneratedCodeAttribute("slice2cs", "3.4.2")]
    public sealed class FoupMovePrxHelper : Ice.ObjectPrxHelperBase, FoupMovePrx
    {
        #region Synchronous operations

        public int Move(int FoupID, int From, int To)
        {
            return Move(FoupID, From, To, null, false);
        }

        public int Move(int FoupID, int From, int To, _System.Collections.Generic.Dictionary<string, string> context__)
        {
            return Move(FoupID, From, To, context__, true);
        }

        private int Move(int FoupID, int From, int To, _System.Collections.Generic.Dictionary<string, string> context__, bool explicitContext__)
        {
            if(explicitContext__ && context__ == null)
            {
                context__ = emptyContext_;
            }
            int cnt__ = 0;
            while(true)
            {
                Ice.ObjectDel_ delBase__ = null;
                try
                {
                    checkTwowayOnly__("Move");
                    delBase__ = getDelegate__(false);
                    FoupMoveDel_ del__ = (FoupMoveDel_)delBase__;
                    return del__.Move(FoupID, From, To, context__);
                }
                catch(IceInternal.LocalExceptionWrapper ex__)
                {
                    handleExceptionWrapperRelaxed__(delBase__, ex__, true, ref cnt__);
                }
                catch(Ice.LocalException ex__)
                {
                    handleException__(delBase__, ex__, true, ref cnt__);
                }
            }
        }

        #endregion

        #region Asynchronous operations

        public Ice.AsyncResult<MCS.Callback_FoupMove_Move> begin_Move(int FoupID, int From, int To)
        {
            return begin_Move(FoupID, From, To, null, false, null, null);
        }

        public Ice.AsyncResult<MCS.Callback_FoupMove_Move> begin_Move(int FoupID, int From, int To, _System.Collections.Generic.Dictionary<string, string> ctx__)
        {
            return begin_Move(FoupID, From, To, ctx__, true, null, null);
        }

        public Ice.AsyncResult begin_Move(int FoupID, int From, int To, Ice.AsyncCallback cb__, object cookie__)
        {
            return begin_Move(FoupID, From, To, null, false, cb__, cookie__);
        }

        public Ice.AsyncResult begin_Move(int FoupID, int From, int To, _System.Collections.Generic.Dictionary<string, string> ctx__, Ice.AsyncCallback cb__, object cookie__)
        {
            return begin_Move(FoupID, From, To, ctx__, true, cb__, cookie__);
        }

        private const string __Move_name = "Move";

        public int end_Move(Ice.AsyncResult r__)
        {
            IceInternal.OutgoingAsync outAsync__ = (IceInternal.OutgoingAsync)r__;
            IceInternal.OutgoingAsync.check__(outAsync__, this, __Move_name);
            if(!outAsync__.wait__())
            {
                try
                {
                    outAsync__.throwUserException__();
                }
                catch(Ice.UserException ex__)
                {
                    throw new Ice.UnknownUserException(ex__.ice_name(), ex__);
                }
            }
            int ret__;
            IceInternal.BasicStream is__ = outAsync__.istr__;
            is__.startReadEncaps();
            ret__ = is__.readInt();
            is__.endReadEncaps();
            return ret__;
        }

        private Ice.AsyncResult<MCS.Callback_FoupMove_Move> begin_Move(int FoupID, int From, int To, _System.Collections.Generic.Dictionary<string, string> ctx__, bool explicitContext__, Ice.AsyncCallback cb__, object cookie__)
        {
            checkAsyncTwowayOnly__(__Move_name);
            IceInternal.TwowayOutgoingAsync<MCS.Callback_FoupMove_Move> result__ =  new IceInternal.TwowayOutgoingAsync<MCS.Callback_FoupMove_Move>(this, __Move_name, Move_completed__, cookie__);
            if(cb__ != null)
            {
                result__.whenCompletedWithAsyncCallback(cb__);
            }
            try
            {
                result__.prepare__(__Move_name, Ice.OperationMode.Idempotent, ctx__, explicitContext__);
                IceInternal.BasicStream os__ = result__.ostr__;
                os__.writeInt(FoupID);
                os__.writeInt(From);
                os__.writeInt(To);
                os__.endWriteEncaps();
                result__.send__(true);
            }
            catch(Ice.LocalException ex__)
            {
                result__.exceptionAsync__(ex__);
            }
            return result__;
        }

        private void Move_completed__(Ice.AsyncResult r__, MCS.Callback_FoupMove_Move cb__, Ice.ExceptionCallback excb__)
        {
            int ret__;
            try
            {
                ret__ = end_Move(r__);
            }
            catch(Ice.Exception ex__)
            {
                if(excb__ != null)
                {
                    excb__(ex__);
                }
                return;
            }
            if(cb__ != null)
            {
                cb__(ret__);
            }
        }

        #endregion

        #region Checked and unchecked cast operations

        public static FoupMovePrx checkedCast(Ice.ObjectPrx b)
        {
            if(b == null)
            {
                return null;
            }
            FoupMovePrx r = b as FoupMovePrx;
            if((r == null) && b.ice_isA(ice_staticId()))
            {
                FoupMovePrxHelper h = new FoupMovePrxHelper();
                h.copyFrom__(b);
                r = h;
            }
            return r;
        }

        public static FoupMovePrx checkedCast(Ice.ObjectPrx b, _System.Collections.Generic.Dictionary<string, string> ctx)
        {
            if(b == null)
            {
                return null;
            }
            FoupMovePrx r = b as FoupMovePrx;
            if((r == null) && b.ice_isA(ice_staticId(), ctx))
            {
                FoupMovePrxHelper h = new FoupMovePrxHelper();
                h.copyFrom__(b);
                r = h;
            }
            return r;
        }

        public static FoupMovePrx checkedCast(Ice.ObjectPrx b, string f)
        {
            if(b == null)
            {
                return null;
            }
            Ice.ObjectPrx bb = b.ice_facet(f);
            try
            {
                if(bb.ice_isA(ice_staticId()))
                {
                    FoupMovePrxHelper h = new FoupMovePrxHelper();
                    h.copyFrom__(bb);
                    return h;
                }
            }
            catch(Ice.FacetNotExistException)
            {
            }
            return null;
        }

        public static FoupMovePrx checkedCast(Ice.ObjectPrx b, string f, _System.Collections.Generic.Dictionary<string, string> ctx)
        {
            if(b == null)
            {
                return null;
            }
            Ice.ObjectPrx bb = b.ice_facet(f);
            try
            {
                if(bb.ice_isA(ice_staticId(), ctx))
                {
                    FoupMovePrxHelper h = new FoupMovePrxHelper();
                    h.copyFrom__(bb);
                    return h;
                }
            }
            catch(Ice.FacetNotExistException)
            {
            }
            return null;
        }

        public static FoupMovePrx uncheckedCast(Ice.ObjectPrx b)
        {
            if(b == null)
            {
                return null;
            }
            FoupMovePrx r = b as FoupMovePrx;
            if(r == null)
            {
                FoupMovePrxHelper h = new FoupMovePrxHelper();
                h.copyFrom__(b);
                r = h;
            }
            return r;
        }

        public static FoupMovePrx uncheckedCast(Ice.ObjectPrx b, string f)
        {
            if(b == null)
            {
                return null;
            }
            Ice.ObjectPrx bb = b.ice_facet(f);
            FoupMovePrxHelper h = new FoupMovePrxHelper();
            h.copyFrom__(bb);
            return h;
        }

        public static readonly string[] ids__ =
        {
            "::Ice::Object",
            "::MCS::FoupMove"
        };

        public static string ice_staticId()
        {
            return ids__[1];
        }

        #endregion

        #region Marshaling support

        protected override Ice.ObjectDelM_ createDelegateM__()
        {
            return new FoupMoveDelM_();
        }

        protected override Ice.ObjectDelD_ createDelegateD__()
        {
            return new FoupMoveDelD_();
        }

        public static void write__(IceInternal.BasicStream os__, FoupMovePrx v__)
        {
            os__.writeProxy(v__);
        }

        public static FoupMovePrx read__(IceInternal.BasicStream is__)
        {
            Ice.ObjectPrx proxy = is__.readProxy();
            if(proxy != null)
            {
                FoupMovePrxHelper result = new FoupMovePrxHelper();
                result.copyFrom__(proxy);
                return result;
            }
            return null;
        }

        #endregion
    }
}

namespace MCS
{
    [_System.CodeDom.Compiler.GeneratedCodeAttribute("slice2cs", "3.4.2")]
    public interface FoupMoveDel_ : Ice.ObjectDel_
    {
        int Move(int FoupID, int From, int To, _System.Collections.Generic.Dictionary<string, string> context__);
    }
}

namespace MCS
{
    [_System.Runtime.InteropServices.ComVisible(false)]
    [_System.CodeDom.Compiler.GeneratedCodeAttribute("slice2cs", "3.4.2")]
    public sealed class FoupMoveDelM_ : Ice.ObjectDelM_, FoupMoveDel_
    {
        public int Move(int FoupID, int From, int To, _System.Collections.Generic.Dictionary<string, string> context__)
        {
            IceInternal.Outgoing og__ = handler__.getOutgoing("Move", Ice.OperationMode.Idempotent, context__);
            try
            {
                try
                {
                    IceInternal.BasicStream os__ = og__.ostr();
                    os__.writeInt(FoupID);
                    os__.writeInt(From);
                    os__.writeInt(To);
                }
                catch(Ice.LocalException ex__)
                {
                    og__.abort(ex__);
                }
                bool ok__ = og__.invoke();
                try
                {
                    if(!ok__)
                    {
                        try
                        {
                            og__.throwUserException();
                        }
                        catch(Ice.UserException ex__)
                        {
                            throw new Ice.UnknownUserException(ex__.ice_name(), ex__);
                        }
                    }
                    IceInternal.BasicStream is__ = og__.istr();
                    is__.startReadEncaps();
                    int ret__;
                    ret__ = is__.readInt();
                    is__.endReadEncaps();
                    return ret__;
                }
                catch(Ice.LocalException ex__)
                {
                    throw new IceInternal.LocalExceptionWrapper(ex__, false);
                }
            }
            finally
            {
                handler__.reclaimOutgoing(og__);
            }
        }
    }
}

namespace MCS
{
    [_System.Runtime.InteropServices.ComVisible(false)]
    [_System.CodeDom.Compiler.GeneratedCodeAttribute("slice2cs", "3.4.2")]
    public sealed class FoupMoveDelD_ : Ice.ObjectDelD_, FoupMoveDel_
    {
        [_System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031")]
        public int Move(int FoupID, int From, int To, _System.Collections.Generic.Dictionary<string, string> context__)
        {
            Ice.Current current__ = new Ice.Current();
            initCurrent__(ref current__, "Move", Ice.OperationMode.Idempotent, context__);
            int result__ = 0;
            IceInternal.Direct.RunDelegate run__ = delegate(Ice.Object obj__)
            {
                FoupMove servant__ = null;
                try
                {
                    servant__ = (FoupMove)obj__;
                }
                catch(_System.InvalidCastException)
                {
                    throw new Ice.OperationNotExistException(current__.id, current__.facet, current__.operation);
                }
                result__ = servant__.Move(FoupID, From, To, current__);
                return Ice.DispatchStatus.DispatchOK;
            };
            IceInternal.Direct direct__ = null;
            try
            {
                direct__ = new IceInternal.Direct(current__, run__);
                try
                {
                    Ice.DispatchStatus status__ = direct__.servant().collocDispatch__(direct__);
                    _System.Diagnostics.Debug.Assert(status__ == Ice.DispatchStatus.DispatchOK);
                }
                finally
                {
                    direct__.destroy();
                }
            }
            catch(Ice.SystemException)
            {
                throw;
            }
            catch(_System.Exception ex__)
            {
                IceInternal.LocalExceptionWrapper.throwWrapper(ex__);
            }
            return result__;
        }
    }
}

namespace MCS
{
    [_System.Runtime.InteropServices.ComVisible(false)]
    [_System.CodeDom.Compiler.GeneratedCodeAttribute("slice2cs", "3.4.2")]
    public abstract class FoupMoveDisp_ : Ice.ObjectImpl, FoupMove
    {
        #region Slice operations

        public int Move(int FoupID, int From, int To)
        {
            return Move(FoupID, From, To, Ice.ObjectImpl.defaultCurrent);
        }

        public abstract int Move(int FoupID, int From, int To, Ice.Current current__);

        #endregion

        #region Slice type-related members

        public static new readonly string[] ids__ = 
        {
            "::Ice::Object",
            "::MCS::FoupMove"
        };

        public override bool ice_isA(string s)
        {
            return _System.Array.BinarySearch(ids__, s, IceUtilInternal.StringUtil.OrdinalStringComparer) >= 0;
        }

        public override bool ice_isA(string s, Ice.Current current__)
        {
            return _System.Array.BinarySearch(ids__, s, IceUtilInternal.StringUtil.OrdinalStringComparer) >= 0;
        }

        public override string[] ice_ids()
        {
            return ids__;
        }

        public override string[] ice_ids(Ice.Current current__)
        {
            return ids__;
        }

        public override string ice_id()
        {
            return ids__[1];
        }

        public override string ice_id(Ice.Current current__)
        {
            return ids__[1];
        }

        public static new string ice_staticId()
        {
            return ids__[1];
        }

        #endregion

        #region Operation dispatch

        [_System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011")]
        public static Ice.DispatchStatus Move___(FoupMove obj__, IceInternal.Incoming inS__, Ice.Current current__)
        {
            checkMode__(Ice.OperationMode.Idempotent, current__.mode);
            IceInternal.BasicStream is__ = inS__.istr();
            is__.startReadEncaps();
            int FoupID;
            FoupID = is__.readInt();
            int From;
            From = is__.readInt();
            int To;
            To = is__.readInt();
            is__.endReadEncaps();
            IceInternal.BasicStream os__ = inS__.ostr();
            int ret__ = obj__.Move(FoupID, From, To, current__);
            os__.writeInt(ret__);
            return Ice.DispatchStatus.DispatchOK;
        }

        private static string[] all__ =
        {
            "Move",
            "ice_id",
            "ice_ids",
            "ice_isA",
            "ice_ping"
        };

        public override Ice.DispatchStatus dispatch__(IceInternal.Incoming inS__, Ice.Current current__)
        {
            int pos = _System.Array.BinarySearch(all__, current__.operation, IceUtilInternal.StringUtil.OrdinalStringComparer);
            if(pos < 0)
            {
                throw new Ice.OperationNotExistException(current__.id, current__.facet, current__.operation);
            }

            switch(pos)
            {
                case 0:
                {
                    return Move___(this, inS__, current__);
                }
                case 1:
                {
                    return ice_id___(this, inS__, current__);
                }
                case 2:
                {
                    return ice_ids___(this, inS__, current__);
                }
                case 3:
                {
                    return ice_isA___(this, inS__, current__);
                }
                case 4:
                {
                    return ice_ping___(this, inS__, current__);
                }
            }

            _System.Diagnostics.Debug.Assert(false);
            throw new Ice.OperationNotExistException(current__.id, current__.facet, current__.operation);
        }

        #endregion

        #region Marshaling support

        public override void write__(IceInternal.BasicStream os__)
        {
            os__.writeTypeId(ice_staticId());
            os__.startWriteSlice();
            os__.endWriteSlice();
            base.write__(os__);
        }

        public override void read__(IceInternal.BasicStream is__, bool rid__)
        {
            if(rid__)
            {
                /* string myId = */ is__.readTypeId();
            }
            is__.startReadSlice();
            is__.endReadSlice();
            base.read__(is__, true);
        }

        public override void write__(Ice.OutputStream outS__)
        {
            Ice.MarshalException ex = new Ice.MarshalException();
            ex.reason = "type MCS::FoupMove was not generated with stream support";
            throw ex;
        }

        public override void read__(Ice.InputStream inS__, bool rid__)
        {
            Ice.MarshalException ex = new Ice.MarshalException();
            ex.reason = "type MCS::FoupMove was not generated with stream support";
            throw ex;
        }

        #endregion
    }
}
