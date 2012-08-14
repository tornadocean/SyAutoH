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
// Generated from file `Stats.ice'
//
// Warning: do not edit this file.
//
// </auto-generated>
//

#ifndef __Ice_Stats_h__
#define __Ice_Stats_h__

#include <Ice/LocalObjectF.h>
#include <Ice/ProxyF.h>
#include <Ice/ObjectF.h>
#include <Ice/Exception.h>
#include <Ice/LocalObject.h>
#include <IceUtil/ScopedArray.h>
#include <Ice/UndefSysMacros.h>

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

#ifndef ICE_API
#   ifdef ICE_API_EXPORTS
#       define ICE_API ICE_DECLSPEC_EXPORT
#   else
#       define ICE_API ICE_DECLSPEC_IMPORT
#   endif
#endif

namespace Ice
{

class Stats;
bool operator==(const Stats&, const Stats&);
bool operator<(const Stats&, const Stats&);

}

namespace IceInternal
{

ICE_API ::Ice::LocalObject* upCast(::Ice::Stats*);

}

namespace Ice
{

typedef ::IceInternal::Handle< ::Ice::Stats> StatsPtr;

}

namespace Ice
{

class ICE_API Stats : virtual public ::Ice::LocalObject
{
public:

    typedef StatsPtr PointerType;
    

    virtual void bytesSent(const ::std::string&, ::Ice::Int) = 0;

    virtual void bytesReceived(const ::std::string&, ::Ice::Int) = 0;
};

inline bool operator==(const Stats& l, const Stats& r)
{
    return static_cast<const ::Ice::LocalObject&>(l) == static_cast<const ::Ice::LocalObject&>(r);
}

inline bool operator<(const Stats& l, const Stats& r)
{
    return static_cast<const ::Ice::LocalObject&>(l) < static_cast<const ::Ice::LocalObject&>(r);
}

}

#endif
