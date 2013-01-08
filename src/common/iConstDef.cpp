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
// Generated from file `iConstDef.ice'
//
// Warning: do not edit this file.
//
// </auto-generated>
//

#include <stdafx.h>
#include <iConstDef.h>
#include <Ice/BasicStream.h>
#include <Ice/Object.h>
#include <IceUtil/Iterator.h>

#ifndef ICE_IGNORE_VERSION
#   if ICE_INT_VERSION / 100 != 304
#       error Ice version mismatch!
#   endif
#   if ICE_INT_VERSION % 100 > 50
#       error Beta header file detected
#   endif
#   if ICE_INT_VERSION % 100 < 2
#       error Ice patch level mismatch!
#   endif
#endif

void
MCS::dbcli::__write(::IceInternal::BasicStream* __os, ::MCS::dbcli::LoctionType v)
{
    __os->write(static_cast< ::Ice::Byte>(v), 5);
}

void
MCS::dbcli::__read(::IceInternal::BasicStream* __is, ::MCS::dbcli::LoctionType& v)
{
    ::Ice::Byte val;
    __is->read(val, 5);
    v = static_cast< ::MCS::dbcli::LoctionType>(val);
}

void
MCS::GuiHub::__write(::IceInternal::BasicStream* __os, ::MCS::GuiHub::MesTransCtrl v)
{
    __os->write(static_cast< ::Ice::Byte>(v), 4);
}

void
MCS::GuiHub::__read(::IceInternal::BasicStream* __is, ::MCS::GuiHub::MesTransCtrl& v)
{
    ::Ice::Byte val;
    __is->read(val, 4);
    v = static_cast< ::MCS::GuiHub::MesTransCtrl>(val);
}

void
MCS::GuiHub::__write(::IceInternal::BasicStream* __os, ::MCS::GuiHub::GuiCommand v)
{
    __os->write(static_cast< ::Ice::Byte>(v), 24);
}

void
MCS::GuiHub::__read(::IceInternal::BasicStream* __is, ::MCS::GuiHub::GuiCommand& v)
{
    ::Ice::Byte val;
    __is->read(val, 24);
    v = static_cast< ::MCS::GuiHub::GuiCommand>(val);
}

void
MCS::GuiHub::__write(::IceInternal::BasicStream* __os, ::MCS::GuiHub::PushData v)
{
    __os->write(static_cast< ::Ice::Byte>(v), 13);
}

void
MCS::GuiHub::__read(::IceInternal::BasicStream* __is, ::MCS::GuiHub::PushData& v)
{
    ::Ice::Byte val;
    __is->read(val, 13);
    v = static_cast< ::MCS::GuiHub::PushData>(val);
}

void
MCS::GuiHub::__writeGuiPushDataList(::IceInternal::BasicStream* __os, const ::MCS::GuiHub::PushData* begin, const ::MCS::GuiHub::PushData* end)
{
    ::Ice::Int size = static_cast< ::Ice::Int>(end - begin);
    __os->writeSize(size);
    for(int i = 0; i < size; ++i)
    {
        ::MCS::GuiHub::__write(__os, begin[i]);
    }
}

void
MCS::GuiHub::__readGuiPushDataList(::IceInternal::BasicStream* __is, ::MCS::GuiHub::GuiPushDataList& v)
{
    ::Ice::Int sz;
    __is->readAndCheckSeqSize(1, sz);
    v.resize(sz);
    for(int i = 0; i < sz; ++i)
    {
        ::MCS::GuiHub::__read(__is, v[i]);
    }
}
