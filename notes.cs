// Go over design - will it still compose the same way if the 
// transport layer is switched to sockets?  Does the http part
// make assumptions about transport that sockets doesn't (easily)
// support.

// Static Debug class - thread safe, should only be able to be called when building 
// in DEBUG.  RELEASE builds should throw a DeveloperException.  Actually, just have
// the definition in DEBUG ifdef, and calls to it in Debug.......  Should be extensible 
// via bootstrap code, so can write debug info to anything.

// Everything should be composable.

// Everything should be able to be encapsulated by something else and not be aware of it.

// Nothing should be coupled to any specific implimentation.

// Closures closures everywhere.

// Always test against interfaces, even if you only have a single implementation.

// Only use libraries if they cut down on boiler-plate code (e.g. automapper).

// static extensions methods that act against interfaces

// All is (sadly) JSON in the Http message's body:
//  - predictable bloody endpoint names
//  - consistency - always body, never a mix
//  - allows for generic validation and mapping (optimise for both machine and coder)
//  - this is an API, therefore we should be optimising for 
//    machines.  Web sites should be optimised for people.
//    Instead, we optimise APIs for people, and websites 
//    for views, stats and whatever "front-end framework
//    paradigm" is currently the most popular.
//    The same people who make APIs are the same people who consume
//    them, so they tailor the API for their use.  The same people 
//    make websites, which they do not consume, and yet they still
//    tailor them for their "use".  That use being the making and 
//    maintaining of said website.
//  - But the code, my friend, THAT should be written for humans.  If it's too hard, make an abstraction to work with and repeat.
//  - Everything is a request - request to PUT, DELETE, UPDATE etc., so make that an abstraction.
//  - Still human readable, but WAY better suited to machines AND generic code.

// "Let it crash" framework, similar to BEAM - DeveloperException will ALWAYS kill the main process.

// Each config entry must be a record.  Set defaults in the default constructor.
// Can we constrain on record?

// An endpoint can ask for API specific config in the constructor, and only what has been set up
// as a 'default' for things like database access (only ever ask for repository, for instance).  There's
// no reason that any endpoint should require access to the entire bloody configuration file.

// standardise on allowed HTTP codes, so can abstract away into success/failure etc.
// Always return 200, with return code a user-supplied enum?  Even required?

// Put pipes behind interfaces, then start sending messages.

// Can then scale across processes / machines.

// Add a before/after filter/hook for things like HTTP headers?  

// Can I use async/await with Task?  Can the thread-pool make use of it?

// By abstracting away the "how" - i.e. JSON and a fixed format, the logic
// can actually focus on the logic.

// Treat HTTPS as the transport layer, i.e. create an abstraction that 
// allows you to treat it as such, so you can start ignoring it.

// Abstractions let you ignore the boring bits.

// Should be easy to add before/after for things like audit logging at both API
// and endpoint level.

// Config should have defaults, run with --create-config to serialise to 
// a default config file.

// Using ApiConfiguration, because don't want people accessing the server directly, 
// but via a mediator.  The bootstrap class is responsible for creation of both,
// and the hand-off.  It's a top-level mediator.

// Must be able to impelement "swagger" like functionality using standard endpoint behavior.

// The threadpool etc, in fact a generic collection pool, should be available as services at 
// the API level.  Not sure about endpoints though...

// Use the bootstrap code to then load in tables of types to build up the server in a loop.  Note that 
// tests can then become the exact same tables, but with a couple of dpendencies swapped out.

// Move cache out of IAnswer - setting cache tuning should be done in one place - match answer to cache
// settings.  Also, cache should not be a concern of the handler.  It's there to answer quetions, not 
// say what you can do with them.

// Websockets API.

// Error handling.

// API to fuck with configuration?  At least have a dashboard - use web sockets to update.

// Make sure api/endpoint paths are unique.

// Add a carertaker thread.

// Add a X509StoreService (or certificate service).

// Can I use a single store interface for all three OSs?

// Everything in bootstrap/configuration etc can be viewed as a function - the private vars
// are like a let statement in lisp.  This analogy only works if they are ONLY used like
// so: create -> set privates -> use privates to create a "result" value, throw away object.
// For instance, if you assign the result of Done() like:
// var fred = new builder().DoStuffWithBuilder().Build();
// then the builder (i.e. intermediate values) can be treated semantically as a function.  It's 
// just a long-winded way of expressing one.

// RESTful API - let's focus on the API bit.  REST is just how we're doing something.  It's
// nowhere near as important as WHY we're doing something.  In other words, HTTP should never even
// be mentioned, ANYWHERE in your Endpoint handlers.

// Built in session management in the form of session tokens.  Is up to the handler and client
// to make it meaningful.

// HTTP headers are a part of the transport concern - endpoint handlers shouldn't even KNOW
// when http is, that they're running inside a web service.  Which leads to:

// Leaky abstractsion.  Every abstraction leaks to some degree.  The leak I want to plug
// here, is that the code is running inside a web service.  That's it.  Endpoint handler code should
// have ZERO idea about the context that it's running in.  It's handed a question, it asks for 0-many
// resources to help it answer that question, and it returns an answer.
// Therefore: no HttpContext.

// Every time that I find myself thinking "asp.net has this, so should Fred", STOP, and ask why
// it has it in the first place.  Maybe it's like HttpContext - i.e. a leaky abstraction - in which
// case, figure out WHY it's used, and therefore how it can map onto existing abstrations.

// Really try to push home that "static = evil" just means that you don't know how to
// use them properly.  i.e. as a way around the short-commings of traditional OO development
// practices.